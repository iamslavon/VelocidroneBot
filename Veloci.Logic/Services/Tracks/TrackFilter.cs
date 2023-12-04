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

        //From old bot
        new Regex("collision", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("redbull dr.one", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("vrl season 3 track 3", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("vrl team championships", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("growers rock garden", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("vrl season 7 championships", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("gokartrelay", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("gods_of_quadhalla", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("vrl-freestyle-country", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("boners journey", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("world of war", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("corona", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("neon cage", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("tbs spec 4", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("tdl races - gamex 2019", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("^opg", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("boners bando towers", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("vrl-freestyle-coast", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("boners bonsai fpv 4 freestyle", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("freestyle_tower_of_magical_power", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("dragons_and_wizards", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("trainer", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("tropical heat", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex("rona masters", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    ];

    public bool IsTrackGoodFor5inchRacing(ParsedTrackModel track)
    {
        if (_blackListedTracks.Any(b => b.IsMatch(track.Name)))
            return false;

        return true;
    }
}
