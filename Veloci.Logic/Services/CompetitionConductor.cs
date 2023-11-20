using Microsoft.EntityFrameworkCore;
using Serilog;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot;

namespace Veloci.Logic.Services;

public class CompetitionConductor
{
    private readonly IRepository<Competition> _competitions;
    private readonly IRepository<Track> _tracks;
    private readonly IRepository<TrackMap> _maps;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;
    private readonly CompetitionService _competitionService;
    private readonly MessageComposer _messageComposer;

    public CompetitionConductor(
        IRepository<Competition> competitions,
        IRepository<Track> tracks,
        IRepository<TrackMap> maps,
        ResultsFetcher resultsFetcher,
        RaceResultsConverter resultsConverter,
        CompetitionService competitionService,
        MessageComposer messageComposer)
    {
        _competitions = competitions;
        _tracks = tracks;
        _maps = maps;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _competitionService = competitionService;
        _messageComposer = messageComposer;
    }

    public async Task StartNewAsync(string message, long chatId)
    {
        Log.Debug("Starting a new competition");

        var activeComp = await GetActiveCompetitionAsync(chatId);

        if (activeComp is not null)
            throw new Exception("Competition for this chat is already started");

        var trackId = MessageParser.GetTrackId(message);
        var mapTrackName = MessageParser.GetTrackName(message);

        var track = await _tracks
                        .GetAll()
                        .FirstOrDefaultAsync(t => t.TrackId == trackId)
                    ?? await CreateNewTrackAsync(mapTrackName.map, mapTrackName.track, trackId);

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
            ChatId = chatId,
            InitialResults = trackResults,
            CurrentResults = trackResults
        };

        await _competitions.AddAsync(competition);
    }

    public async Task StopAsync(string message, long chatId, int messageId)
    {
        Log.Debug("Stopping a competition");

        var competition = await GetActiveCompetitionAsync(chatId);

        if (competition is null)
            throw new Exception("There are no active competitions for this chat");

        if (!message.Contains(competition.Track.FullName))
            throw new Exception("Can not stop competition. Active one is on another track");

        competition.State = CompetitionState.Closed;
        competition.CompetitionResults = _competitionService.GetLocalLeaderboard(competition);
        await _competitions.SaveChangesAsync();

        var resultsMessage = _messageComposer.Leaderboard(competition.CompetitionResults, competition.Track.FullName);
        await TelegramBot.EditMessageAsync(resultsMessage, chatId, messageId);
    }

    public async Task TempSeasonResultsAsync(long chatId, int messageId)
    {
        var today = DateTime.Now;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
        var results = await _competitionService.GetSeasonResultsAsync(chatId, firstDayOfMonth, today);
        var message = _messageComposer.TempSeasonResults(results);

        // TODO: Uncomment in the next month
        //await TelegramBot.EditMessageAsync(message, chatId, messageId);
    }

    public async Task StopSeasonAsync(long chatId, int messageId)
    {
        var today = DateTime.Now;
        var firstDayOfPreviousMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
        var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

        var results =
            await _competitionService.GetSeasonResultsAsync(chatId, firstDayOfPreviousMonth, firstDayOfCurrentMonth);
        var message = _messageComposer.SeasonResults(results);

        // TODO: Uncomment in the next month
        //await TelegramBot.EditMessageAsync(message, chatId, messageId);
    }

    private async Task<Competition?> GetActiveCompetitionAsync(long chatId)
    {
        return await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .FirstOrDefaultAsync(c => c.ChatId == chatId);
    }

    private async Task<Track> CreateNewTrackAsync(string mapName, string trackName, int trackId)
    {
        var dbMap = await _maps
                        .GetAll()
                        .FirstOrDefaultAsync(m => m.Name == mapName)
                    ?? await CreateNewMapAsync(mapName);

        var track = new Track
        {
            MapId = dbMap.Id,
            Name = trackName,
            TrackId = trackId
        };

        await _tracks.AddAsync(track);

        return track;
    }

    private async Task<TrackMap> CreateNewMapAsync(string name)
    {
        var map = new TrackMap
        {
            Name = name
        };

        await _maps.AddAsync(map);

        return map;
    }
}
