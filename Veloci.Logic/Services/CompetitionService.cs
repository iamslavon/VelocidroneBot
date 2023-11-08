using Microsoft.EntityFrameworkCore;
using Veloci.Data.Repositories;
using Veloci.Logic.Domain;

namespace Veloci.Logic.Services;

public class CompetitionService
{
    private readonly IRepository<Competition> _competitions;
    private readonly IRepository<Track> _tracks;
    private readonly IRepository<TrackMap> _maps;
    private readonly ResultsFetcher _resultsFetcher;
    private readonly RaceResultsConverter _resultsConverter;

    public CompetitionService(
        IRepository<Competition> competitions, 
        ResultsFetcher resultsFetcher, 
        RaceResultsConverter resultsConverter, 
        IRepository<Track> tracks, 
        IRepository<TrackMap> maps)
    {
        _competitions = competitions;
        _resultsFetcher = resultsFetcher;
        _resultsConverter = resultsConverter;
        _tracks = tracks;
        _maps = maps;
    }

    public async Task UpdateResultsAsync()
    {
        var activeCompetitions = await _competitions
            .GetAll(c => c.State == CompetitionState.Started)
            .ToListAsync();

        if (!activeCompetitions.Any())
            return;
        
        foreach (var competition in activeCompetitions)
        {
            await UpdateResults(competition);
        }
    }

    private async Task UpdateResults(Competition competition)
    {
        var resultsDto = await _resultsFetcher.FetchAsync(competition.Track.TrackId);
        var results = _resultsConverter.ConvertTrackTimes(resultsDto);

        // calculate deltas, send message
        
        competition.CurrentResults = new TrackResults
        {
            Times = results
        };
    }
    
    public async Task StartNewAsync(string message, long chatId)
    {
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
        var competition = await GetActiveCompetitionAsync(chatId);

        if (competition is null)
            throw new Exception("There are no active competitions for this chat");
        
        if (!message.Contains(competition.Track.FullName))
            throw new Exception("Can not stop competition. Active one is on another track");

        competition.State = CompetitionState.Closed;
        await _competitions.SaveChangesAsync();
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