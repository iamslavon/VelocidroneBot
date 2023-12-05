using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot;
using Veloci.Logic.Services.Tracks;

namespace Veloci.Logic.Services;

public class CompetitionConductor
{
    private readonly IRepository<Competition> _competitions;
    private readonly TrackService _trackService;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;
    private readonly CompetitionService _competitionService;
    private readonly MessageComposer _messageComposer;
    private readonly ImageService _imageService;

    public CompetitionConductor(
        IRepository<Competition> competitions,
        ResultsFetcher resultsFetcher,
        RaceResultsConverter resultsConverter,
        CompetitionService competitionService,
        MessageComposer messageComposer,
        ImageService imageService,
        TrackService trackService)
    {
        _competitions = competitions;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _competitionService = competitionService;
        _messageComposer = messageComposer;
        _imageService = imageService;
        _trackService = trackService;
    }

    public async Task StartNewAsync()
    {
        Log.Debug("Starting a new competition");

        var activeComp = await GetActiveCompetitionAsync();

        if (activeComp is not null)
        {
            await StopPollAsync();
            await CancelAsync();
        }

        var track = await _trackService.GetRandomTrackAsync();
        var resultsDto = await _resultsFetcher.FetchAsync(track.TrackId);
        var results = _resultsConverter.ConvertTrackTimes(resultsDto);
        var trackResults = new TrackResults
        {
            Times = results
        };

        var competition = new Competition
        {
            TrackId = track.Id,
            State = CompetitionState.Started,
            InitialResults = trackResults,
            CurrentResults = trackResults
        };

        await _competitions.AddAsync(competition);

        var startCompetitionMessage = _messageComposer.StartCompetition(track);
        await TelegramBot.SendMessageAsync(startCompetitionMessage);

        var poll = _messageComposer.Poll(track.FullName);
        var pollId = await TelegramBot.SendPollAsync(poll);

        if (pollId is null)
            return;

        var rating = competition.Track.Rating;

        if (rating is null)
        {
            rating = new TrackRating();
            competition.Track.Rating = rating;
        }

        rating.PollMessageId = pollId.Value;
        await _competitions.SaveChangesAsync();
    }

    public async Task StopAsync()
    {
        Log.Debug("Stopping a competition");

        var competition = await GetActiveCompetitionAsync();

        if (competition is null)
            throw new Exception("There are no active competitions");

        competition.State = CompetitionState.Closed;
        competition.CompetitionResults = _competitionService.GetLocalLeaderboard(competition);
        await _competitions.SaveChangesAsync();

        var resultsMessage = _messageComposer.Leaderboard(competition.CompetitionResults, competition.Track.FullName);
        await TelegramBot.SendMessageAsync(resultsMessage);
    }

    private async Task CancelAsync()
    {
        Log.Debug("Cancelling a competition");

        var competition = await GetActiveCompetitionAsync();

        if (competition is null)
            throw new Exception("There are no active competitions");

        competition.State = CompetitionState.Cancelled;
        await _competitions.SaveChangesAsync();
    }

    public async Task StopPollAsync()
    {
        Log.Debug("Stopping poll");

        var competition = await GetActiveCompetitionAsync();

        if (competition is null)
            throw new Exception("There are no active competitions");

        var poll = _messageComposer.Poll(competition.Track.FullName);
        var telegramPoll = await TelegramBot.StopPollAsync(competition.Track.Rating.PollMessageId);

        if (telegramPoll is null)
        {
            Log.Error("Poll is already stopped");
            return;
        }

        var totalPoints = telegramPoll.Options.Sum(option =>
        {
            var points = poll.Options.FirstOrDefault(x => x.Text == option.Text).Points;
            return option.VoterCount * points;
        });

        double? rating = telegramPoll.TotalVoterCount == 0 ?
            null :
            totalPoints / telegramPoll.TotalVoterCount;

        competition.Track.Rating.Value = rating;
        await _competitions.SaveChangesAsync();

        if (rating >= 0)
            return;

        var message = _messageComposer.BadTrackRating();
        await TelegramBot.SendMessageAsync(message);
    }

    public async Task SeasonResultsAsync()
    {
        Log.Debug("Publishing season results");

        var now = DateTime.Now;

        if (now.Day == 1)
        {
            await StopSeasonAsync();
        }
        else
        {
            await TempSeasonResultsAsync();
        }
    }

    private async Task TempSeasonResultsAsync()
    {
        var today = DateTime.Now;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
        var results = await _competitionService.GetSeasonResultsAsync(firstDayOfMonth, today);
        var message = _messageComposer.TempSeasonResults(results);
        await TelegramBot.SendMessageAsync(message);
    }

    private async Task StopSeasonAsync()
    {
        var today = DateTime.Now;
        var firstDayOfPreviousMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
        var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

        var results =
            await _competitionService.GetSeasonResultsAsync(firstDayOfPreviousMonth, firstDayOfCurrentMonth);

        if (results.Count == 0)
            return;

        var message = _messageComposer.SeasonResults(results);
        await TelegramBot.SendMessageAsync(message);

        var seasonName = firstDayOfPreviousMonth.ToString("MMMM yyyy");
        var winnerName = results.FirstOrDefault().PlayerName;
        var imageStream = await _imageService.CreateWinnerImageAsync(seasonName, winnerName);
        BackgroundJob.Schedule(() => TelegramBot.SendPhotoAsync(imageStream, null), new TimeSpan(0, 0, 3));

        var medalCountMessage = _messageComposer.MedalCount(results);
        BackgroundJob.Schedule(() => TelegramBot.SendMessageAsync(medalCountMessage), new TimeSpan(0, 0, 6));
    }

    private async Task<Competition?> GetActiveCompetitionAsync()
    {
        return await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .FirstOrDefaultAsync();
    }
}
