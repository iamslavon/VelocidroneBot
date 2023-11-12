using System.Globalization;
using Veloci.Data.Domain;

namespace Veloci.Logic.Services;

public class MessageComposer
{
    public string TimeUpdate(IEnumerable<TrackTimeDelta> deltas)
    {
        var messages = deltas.Select(TimeUpdate);
        return string.Join($"{Environment.NewLine}", messages);
    }

    private string TimeUpdate(TrackTimeDelta delta)
    {
        var timeChangePart = delta.TimeChange.HasValue ? $" ({MsToSec(delta.TimeChange.Value)}s)" : string.Empty;
        var rankOldPart = delta.RankOld.HasValue ? $" (#{delta.RankOld})" : string.Empty;
        
        return $"{delta.PlayerName} - {MsToSec(delta.TrackTime)}s{timeChangePart} / #{delta.Rank}{rankOldPart}";
    }

    public string TimeTable(IEnumerable<TrackTimeDelta> deltas)
    {
        var rows = deltas.Select(TimeRow);
        return string.Join($"{Environment.NewLine}", rows);
    }

    private string TimeRow(TrackTimeDelta time)
    {
        return $"{time.LocalRank} - {time.PlayerName} ({MsToSec(time.TrackTime)}s)";
    } 
    
    private string MsToSec(int ms) => (ms / 1000.0).ToString(CultureInfo.InvariantCulture);
}