using System.Text.Json;
using System.Text.Json.Serialization;
using Veloci.Logic.Services.Tracks.Models;

namespace Veloci.Logic.Services.Tracks;

public class ApiTrackFetcher : ITrackFetcher
{
    private static readonly HttpClient Client = new HttpClient();

    private readonly IDictionary<int, string> _scenes = new Dictionary<int, string>
    {
        {3,"Hangar"},
        {7,"Industrial Wasteland"},
        {8,"Football Stadium"},
        {12,"Countryside"},
        {15,"Subway"},
        {16,"Empty Scene Day"},
        {17,"Empty Scene Night"},
        {18,"NEC Birmingham"},
        {20,"Underground Carpark"},
        {22,"Coastal"},
        {23,"River2"},
        {24,"City"},
        {26,"Large Carpark"},
        {30,"Bando"},
        {31,"IndoorGoKart"},
        {33,"Dynamic Weather"},
        {43,"Future Hangar Empty"},
        {55,"Night Factory 2"},
        {56,"Factory"},
    };

    public async Task<List<ParsedMapModel>> FetchMapsAsync()
    {
        var response = await Client.GetAsync($"{VelocidroneApiConstants.BaseUrl}/api/get_official_tracks");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var decrypted = Encryption.Decrypt(responseBody);
        var results = JsonSerializer.Deserialize<TrackApiResponse>(decrypted);

        var x = from s in _scenes
            join tr in results.Tracks on s.Key equals tr.SceneId into sceneTracks
            select new ParsedMapModel
            {
                Id = s.Key,
                Name = s.Value,
                Url = "???",
                Tracks = sceneTracks.Select(t => new ParsedTrackModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Url = t.Url,
                }).ToList()
            };

        var maps = x.ToList();

        //TODO: Think about it. How can we get rid of this kostyl ?
        foreach (var scene in maps)
        {
            foreach (var track in scene.Tracks)
            {
                track.Map = scene;
            }
        }

        return maps;
    }
}

public class TrackApiResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("success")]
    public bool success { get; set; }

    [JsonPropertyName("tracks")]
    public List<TrackApi> Tracks { get; set; }
}

public class TrackApi
{
    [JsonPropertyName("track_url")]
    public string Url { get; set; }

    [JsonPropertyName("track_name")]
    public string Name { get; set; }

    [JsonPropertyName("track_id")]
    public int Id { get; set; }

    [JsonPropertyName("scene_id")]
    public int SceneId { get; set; }

    [JsonPropertyName("ver")]
    public int Version { get; set; }

    //TODO: convert to date time later
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("count")]
    public double Count { get; set; }
}
