using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot;

namespace Veloci.Logic.Services;

public class CompetitionService
{
    private readonly IRepository<Competition> _competitions;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;
    private readonly RaceResultDeltaAnalyzer _analyzer;
    private readonly MessageComposer _messageComposer;

    public CompetitionService(
        IRepository<Competition> competitions,
        ResultsFetcher resultsFetcher,
        RaceResultsConverter resultsConverter,
        RaceResultDeltaAnalyzer analyzer,
        MessageComposer messageComposer)
    {
        _competitions = competitions;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _analyzer = analyzer;
        _messageComposer = messageComposer;
    }

    [DisableConcurrentExecution("Competition", 60)]
    public async Task UpdateResultsAsync()
    {
        var activeCompetitions = await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .ToListAsync();

        if (!activeCompetitions.Any())
            return;

        foreach (var competition in activeCompetitions)
        {
            await UpdateResultsAsync(competition);
        }
    }

    private async Task UpdateResultsAsync(Competition competition)
    {
        Log.Debug($"Starting updating results for competition {competition.Id}");

        var resultsDto = await _resultsFetcher.FetchAsync(competition.Track.TrackId);
        var times = _resultsConverter.ConvertTrackTimes(resultsDto);
        var results = new TrackResults
        {
            Times = times
        };

        var deltas = _analyzer.CompareResults(competition.CurrentResults, results);

        if (!deltas.Any())
            return;

        competition.CurrentResults = results;
        competition.TimeDeltas.AddRange(deltas);
        competition.ResultsPosted = false;
        await _competitions.SaveChangesAsync();
        var message = _messageComposer.TimeUpdate(deltas);
        await TelegramBot.SendMessageAsync(message);
    }

    [DisableConcurrentExecution("Competition", 60)]
    public async Task PublishCurrentLeaderboardAsync()
    {
        var activeCompetitions = await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .ToListAsync();

        foreach (var activeCompetition in activeCompetitions)
        {
            await PublishCurrentLeaderboardAsync(activeCompetition);
        }
    }

    private async Task PublishCurrentLeaderboardAsync(Competition competition)
    {
        if (competition.ResultsPosted)
            return;

        if (competition.TimeDeltas.Count == 0)
        {
            await SendCheerUpMessageAsync(competition.ChatId, TelegramMessageType.NobodyFlying);
            return;
        }

        var leaderboard = GetLocalLeaderboard(competition);

        if (leaderboard.Count < 2)
            return;

        Log.Debug($"Publishing current leaderboard for competition {competition.Id}");

        var message = _messageComposer.TempLeaderboard(leaderboard);
        await TelegramBot.SendMessageAsync(message);

        competition.ResultsPosted = true;
        await _competitions.SaveChangesAsync();
    }

    public List<CompetitionResults> GetLocalLeaderboard(Competition competition)
    {
        return competition.TimeDeltas
            .GroupBy(d => d.PlayerName)
            .Select(d => d.MinBy(x => x.TrackTime))
            .OrderBy(d => d.TrackTime)
            .Select((x, i) => new CompetitionResults
            {
                CompetitionId = x.CompetitionId,
                PlayerName = x.PlayerName,
                TrackTime = x.TrackTime,
                LocalRank = i + 1,
                GlobalRank = x.Rank,
                Points = PointsByRank(i + 1)
            })
            .ToList();
    }

    public async Task<List<SeasonResult>> GetSeasonResultsAsync(long chatId, DateTime from, DateTime to)
    {
        var results = await _competitions
            .GetAll(comp => comp.ChatId == chatId)
            .Where(comp => comp.StartedOn >= from && comp.StartedOn <= to)
            .SelectMany(comp => comp.CompetitionResults)
            .GroupBy(result => result.PlayerName)
            .Select(group => new SeasonResult
            {
                PlayerName = group.Key,
                Points = group.Sum(r => r.Points),
                GoldenCount = group.Count(r => r.LocalRank == 1),
                SilverCount = group.Count(r => r.LocalRank == 2),
                BronzeCount = group.Count(r => r.LocalRank == 3)
            })
            .OrderByDescending(result => result.Points)
            .ToListAsync();

        for (var i = 0; i < results.Count; i++)
        {
            results[i].Rank = i + 1;
        }

        return results;
    }

    private int PointsByRank(int rank)
    {
        return rank switch
        {
            1 => 85,
            2 => 72,
            3 => 66,
            4 => 60,
            5 => 54,
            6 => 49,
            7 => 44,
            8 => 39,
            9 => 35,
            10 => 31,
            11 => 27,
            12 => 23,
            13 => 19,
            14 => 16,
            15 => 13,
            16 => 10,
            17 => 7,
            18 => 5,
            19 => 3,
            20 => 2,
            _ => 1
        };
    }

    private async Task SendCheerUpMessageAsync(long chatId, TelegramMessageType type)
    {
        var now = DateTime.Now;
        var isDontDisturbTime = now.Hour is < 7 or > 22;

        if (isDontDisturbTime)
            return;

        var cheerUpMessage = TelegramMessages.GetRandomByType(type);

        if (cheerUpMessage is null)
            return;

        if (cheerUpMessage.FileUrl is null && cheerUpMessage.Text is not null)
        {
            await TelegramBot.SendMessageAsync(cheerUpMessage.Text);
            return;
        }

        if (cheerUpMessage.FileUrl is not null)
        {
            await TelegramBot.SendPhotoAsync(chatId, cheerUpMessage.FileUrl, cheerUpMessage.Text);
        }
    }

    public IQueryable<Competition> GetCurrentCompetitions()
    {
        return _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .OrderByDescending(x => x.StartedOn);
    }
}
