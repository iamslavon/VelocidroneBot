using Microsoft.EntityFrameworkCore;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Services.Tracks.Models;

namespace Veloci.Logic.Services.Tracks;

public class TrackService
{
    private readonly IRepository<Track> _tracks;
    private readonly IRepository<TrackMap> _maps;
    private readonly IRepository<Competition> _competitions;
    private readonly TrackFetcher _trackFetcher;
    private List<string>? _usedTrackIds;

    public TrackService(
        TrackFetcher trackFetcher,
        IRepository<Track> tracks,
        IRepository<TrackMap> maps,
        IRepository<Competition> competitions)
    {
        _trackFetcher = trackFetcher;
        _tracks = tracks;
        _maps = maps;
        _competitions = competitions;
    }

    public async Task<Track> GetRandomTrackAsync()
    {
        var maps = await _trackFetcher.FetchMapsAsync();
        return await GetRandomTrackAsync(maps);
    }

    private async Task<Track> GetRandomTrackAsync(List<ParsedTrackModel> maps)
    {
        var map = GetRandomElement(maps);
        var tracks = await _trackFetcher.FetchMapTracksAsync(map.Url);
        var track = GetRandomElement(tracks);
        var dbTrack = await GetTrackAsync(track.Id)
                      ?? await CreateNewTrackAsync(map.Name, map.Id, track.Name, track.Id);

        var usedTrackIds = await GetUsedTrackIdsAsync();

        if (dbTrack.Rating?.Value is < 0 || usedTrackIds.Contains(dbTrack.Id))
        {
            return await GetRandomTrackAsync(maps);
        }

        return dbTrack;
    }

    private async Task<Track?> GetTrackAsync(int trackId)
    {
        return await _tracks
                .GetAll()
                .FirstOrDefaultAsync(t => t.TrackId == trackId);
    }

    private async Task<Track> CreateNewTrackAsync(string mapName, int mapId, string trackName, int trackId)
    {
        var dbMap = await _maps
                        .GetAll()
                        .FirstOrDefaultAsync(m => m.Name == mapName)
                    ?? await CreateNewMapAsync(mapName, mapId);

        if (dbMap.MapId == 0)
            dbMap.MapId = mapId; // since MapId property was added later, some maps dont have this value

        var track = new Track
        {
            MapId = dbMap.Id,
            Name = trackName,
            TrackId = trackId
        };

        await _tracks.AddAsync(track);

        return track;
    }

    private async Task<List<string>> GetUsedTrackIdsAsync()
    {
        if (_usedTrackIds is not null)
            return _usedTrackIds;

        var start = DateTime.Now.AddMonths(-6);

        var ids = await _competitions
            .GetAll(comp => comp.StartedOn > start)
            .Select(comp => comp.Track.Id)
            .ToListAsync();

        _usedTrackIds = ids;

        return ids;
    }

    private async Task<TrackMap> CreateNewMapAsync(string name, int mapId)
    {
        var map = new TrackMap
        {
            Name = name,
            MapId = mapId
        };

        await _maps.AddAsync(map);

        return map;
    }

    private T GetRandomElement<T>(IReadOnlyList<T> list)
    {
        var random = new Random();
        var randomIndex = random.Next(0, list.Count);
        return list[randomIndex];
    }
}
