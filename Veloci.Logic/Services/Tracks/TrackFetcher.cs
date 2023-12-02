using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Veloci.Logic.Services.Tracks.Models;

namespace Veloci.Logic.Services.Tracks;

public class TrackFetcher
{
    private readonly string[] _blackListedMaps =
    {
        "Apartment",
        "CombatPractice",
        "MiniWarehouse",
        "NightClub",
        "Sportbar",
        "House",
        "Office",
        "Sports Hall",
        "Basketball Stadium",
        "Library",
        "Office Complex",
        "Slovenia Krvavec"
    };

    private readonly string[] _blackListedTracks =
    {
        "Pylons",
        "Freestyle"
    };

    public async Task<List<ParsedTrackModel>> FetchMapsAsync()
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync("https://www.velocidrone.com/leaderboards");
        var nodes = doc.DocumentNode.SelectNodes("//a[@class='scenery-card']");
        var maps = nodes.Select(ParseMapNode)
            .Where(x => x is not null)
            .ToList();

        return maps;
    }

    public async Task<List<ParsedTrackModel>> FetchMapTracksAsync(string url)
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(url);
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='track-grid__li']");
        var tracks = nodes.Select(ParseTrackNode)
            .Where(x => x is not null)
            .ToList();

        return tracks;
    }

    private ParsedTrackModel? ParseMapNode(HtmlNode node)
    {
        var nameNode = node.ParentNode
            .Descendants("div")
            .FirstOrDefault(a => a.GetAttributeValue("class", "").Contains("scenery-card__title"));

        var mapName = nameNode.InnerText;

        if (_blackListedMaps.Contains(mapName))
            return null;

        var url = node.GetAttributeValue("href", null);
        var mapId = GetMapId(url);

        return new ParsedTrackModel
        {
            Name = mapName,
            Id = mapId,
            Url = url
        };
    }

    private ParsedTrackModel? ParseTrackNode(HtmlNode node)
    {
        var nameNode = node
            .Descendants("a")
            .FirstOrDefault();

        var mapName = nameNode.InnerText;

        if (_blackListedTracks.Any(mapName.Contains))
            return null;

        var url = nameNode.GetAttributeValue("href", null);
        var trackId = GetTrackId(url);

        return new ParsedTrackModel
        {
            Name = mapName,
            Id = trackId,
            Url = url
        };
    }

    private int GetMapId(string url)
    {
        const string pattern = @"/(\d+)/";
        var match = Regex.Match(url, pattern);

        if (!match.Success)
        {
            throw new Exception("Map id not found");
        }

        var mapIdString = match.Groups[1].Value;

        if (!int.TryParse(mapIdString, out var mapId))
        {
            throw new Exception("Map id parse error");
        }

        return mapId;
    }

    private int GetTrackId(string url)
    {
        const string pattern = @"(\d+)";
        var regex = new Regex(pattern);
        var matches = regex.Matches(url);

        if (matches.Count < 2)
        {
            throw new Exception("Track id not found");
        }

        var mapIdString = matches[1].Groups[1].Value;

        if (!int.TryParse(mapIdString, out var mapId))
        {
            throw new Exception("Track id parse error");
        }

        return mapId;
    }
}
