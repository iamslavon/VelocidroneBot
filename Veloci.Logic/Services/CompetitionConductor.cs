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
            await StopAsync(false);

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

        var pollId = await TelegramBot.SendPollAsync(
            _messageComposer.PollQuestion(track.FullName),
            _messageComposer.PollOptions()
            );

        var rating = new TrackRating { PollMessageId = pollId };
        competition.Track.Rating = rating;
        await _competitions.SaveChangesAsync();
    }

    public async Task StopAsync(bool postResults = true)
    {
        Log.Debug("Stopping a competition");

        var competition = await GetActiveCompetitionAsync();

        if (competition is null)
            throw new Exception("There are no active competitions");

        competition.State = CompetitionState.Closed;
        competition.CompetitionResults = _competitionService.GetLocalLeaderboard(competition);
        await _competitions.SaveChangesAsync();

        if (!postResults)
            return;

        var resultsMessage = _messageComposer.Leaderboard(competition.CompetitionResults, competition.Track.FullName);
        await TelegramBot.SendMessageAsync(resultsMessage);
    }

    public async Task TempSeasonResultsAsync(long chatId, int messageId)
    {
        var today = DateTime.Now;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
        var results = await _competitionService.GetSeasonResultsAsync(chatId, firstDayOfMonth, today);
        var message = _messageComposer.TempSeasonResults(results);
        await TelegramBot.EditMessageAsync(message, chatId, messageId);
    }

    public async Task StopSeasonAsync(long chatId, int messageId)
    {
        var today = DateTime.Now;
        var firstDayOfPreviousMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
        var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

        var results =
            await _competitionService.GetSeasonResultsAsync(chatId, firstDayOfPreviousMonth, firstDayOfCurrentMonth);

        if (results.Count == 0)
            return;

        var message = _messageComposer.SeasonResults(results);
        await TelegramBot.EditMessageAsync(message, chatId, messageId);

        var medalCountMessage = _messageComposer.MedalCount(results);
        BackgroundJob.Schedule(() => TelegramBot.SendMessageAsync(medalCountMessage), new TimeSpan(0, 0, 5));

        var seasonName = firstDayOfPreviousMonth.ToString("MMMM yyyy");
        var winnerName = results.FirstOrDefault().PlayerName;
        var imageStream = await _imageService.CreateWinnerImageAsync(seasonName, winnerName);
        await TelegramBot.SendPhotoAsync(chatId, imageStream);
    }

    private async Task<Competition?> GetActiveCompetitionAsync()
    {
        return await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .FirstOrDefaultAsync();
    }
}
