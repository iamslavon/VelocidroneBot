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
    private readonly IRepository<Track> _tracks;
    private readonly IRepository<TrackMap> _maps;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;
    private readonly RaceResultDeltaAnalyzer _analyzer;
    private readonly MessageComposer _messageComposer;

    public CompetitionService(
        IRepository<Competition> competitions,
        ResultsFetcher resultsFetcher,
        RaceResultsConverter resultsConverter,
        IRepository<Track> tracks,
        IRepository<TrackMap> maps,
        RaceResultDeltaAnalyzer analyzer,
        MessageComposer messageComposer)
    {
        _competitions = competitions;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _tracks = tracks;
        _maps = maps;
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
        await TelegramBot.SendMessageAsync(message, competition.ChatId);
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

    public async Task StopAsync(string message, long chatId)
    {
        Log.Debug("Stopping a competition");

        var competition = await GetActiveCompetitionAsync(chatId);

        if (competition is null)
            throw new Exception("There are no active competitions for this chat");

        if (!message.Contains(competition.Track.FullName))
            throw new Exception("Can not stop competition. Active one is on another track");

        competition.State = CompetitionState.Closed;
        competition.CompetitionResults = GetLocalLeaderboard(competition);
        await _competitions.SaveChangesAsync();
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
        if (!competition.TimeDeltas.Any() || competition.ResultsPosted)
            return;

        var leaderboard = GetLocalLeaderboard(competition);

        if (leaderboard.Count < 2)
            return;

        Log.Debug($"Publishing current leaderboard for competition {competition.Id}");

        var message = _messageComposer.Leaderboard(leaderboard);
        await TelegramBot.SendMessageAsync(message, competition.ChatId);

        competition.ResultsPosted = true;
        await _competitions.SaveChangesAsync();
    }

    private List<CompetitionResults> GetLocalLeaderboard(Competition competition)
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

    public IQueryable<Competition> GetCurrentCompetitions()
    {
        return _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .OrderByDescending(x => x.StartedOn);
    }
}
