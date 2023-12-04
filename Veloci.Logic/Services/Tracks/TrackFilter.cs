using System.Text.RegularExpressions;
using Veloci.Logic.Services.Tracks.Models;

namespace Veloci.Logic.Services.Tracks;

public class TrackFilter
{
    private static readonly Regex[] _blackListedTracks =
    [
        new Regex("Pylons", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Freestyle", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Betafpv", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Beta 2S", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Micro", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("NewBeeDrone", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Toothpick", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Trainer", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("Whoop", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("^level (01,02, 03)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    ];

    public bool IsTrackGoodFor5inchRacing(ParsedTrackModel track)
    {
        if (_blackListedTracks.Any(b => b.IsMatch(track.Name)))
            return false;

        return true;
    }
}
