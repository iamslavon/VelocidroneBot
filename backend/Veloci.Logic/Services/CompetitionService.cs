using System.Linq.Expressions;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot;
using Veloci.Logic.Notifications;

namespace Veloci.Logic.Services;

public class CompetitionService
{
    private readonly IRepository<Competition> _competitions;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;
    private readonly RaceResultDeltaAnalyzer _analyzer;
    private readonly IMediator _mediator;

    public CompetitionService(
        IRepository<Competition> competitions,
        ResultsFetcher resultsFetcher,
        RaceResultsConverter resultsConverter,
        RaceResultDeltaAnalyzer analyzer,
        IMediator mediator)
    {
        _competitions = competitions;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _analyzer = analyzer;
        _mediator = mediator;
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
        Log.Debug("Starting updating results for competition {competitionId}", competition.Id);

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
        await _mediator.Publish(new CurrentResultUpdateMessage(deltas));
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
        if (competition.ResultsPosted) return;

        if (competition.TimeDeltas.Count == 0)
        {
            await SendCheerUpMessageAsync(ChatMessageType.NobodyFlying);
            return;
        }

        var leaderboard = GetLocalLeaderboard(competition);

        if (leaderboard.Count < 2)
        {
            await SendCheerUpMessageAsync(ChatMessageType.OnlyOneFlew);
            return;
        }

        Log.Debug("Publishing current leaderboard for competition {competitionId}", competition.Id);

        await _mediator.Publish(new IntermediateCompetitionResult(leaderboard, competition));

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

    public async Task<List<SeasonResult>> GetSeasonResultsAsync(DateTime from, DateTime to)
    {
        var results = await GetSeasonResultsQuery(from, to)
            .OrderByDescending(result => result.Points)
            .ToListAsync();

        for (var i = 0; i < results.Count; i++)
        {
            results[i].Rank = i + 1;
        }

        return results;
    }

    public IQueryable<SeasonResult> GetSeasonResultsQuery(DateTime from, DateTime to)
    {
        return _competitions
            .GetAll(comp => comp.StartedOn >= from && comp.StartedOn <= to)
            .Where(comp => comp.State != CompetitionState.Cancelled)
            .SelectMany(comp => comp.CompetitionResults)
            .GroupBy(result => result.PlayerName)
            .Select(group => new SeasonResult
            {
                PlayerName = group.Key,
                Points = group.Sum(r => r.Points),
                GoldenCount = group.Count(r => r.LocalRank == 1),
                SilverCount = group.Count(r => r.LocalRank == 2),
                BronzeCount = group.Count(r => r.LocalRank == 3)
            });
    }

    private static int PointsByRank(int rank)
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

    private async Task SendCheerUpMessageAsync(ChatMessageType type)
    {
        if (DoNotDisturb(DateTime.Now)) return;

        var cheerUpMessage = ChatMessages.GetRandomByTypeWithProbability(type);

        if (cheerUpMessage is null) return;

        await _mediator.Publish(new CheerUp(cheerUpMessage));
    }

    private static bool DoNotDisturb(DateTime dateTime)
    {
        return dateTime.Hour is < 7 or > 22;
    }

    public IQueryable<Competition> GetCurrentCompetitions()
    {
        return _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .OrderByDescending(x => x.StartedOn);
    }
}
