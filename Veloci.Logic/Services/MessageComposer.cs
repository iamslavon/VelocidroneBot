using System.Globalization;
using Veloci.Data.Domain;

namespace Veloci.Logic.Services;

public class MessageComposer
{
    public string TimeUpdateMessage(IEnumerable<TrackTimeDelta> deltas)
    {
        var messages = deltas.Select(TimeUpdateMessage);
        return string.Join($"{Environment.NewLine}", messages);
    }

    private string TimeUpdateMessage(TrackTimeDelta delta)
    {
        var timeChangePart = delta.TimeChange.HasValue ? $" ({MsToSec(delta.TimeChange.Value)}s)" : string.Empty;
        var rankOldPart = delta.RankOld.HasValue ? $" (#{delta.RankOld})" : string.Empty;
        
        return $"{delta.PlayerName} - {MsToSec(delta.TrackTime)}s{timeChangePart} / #{delta.Rank}{rankOldPart}";
    }
        
    private string MsToSec(int ms) => (ms / 1000.0).ToString(CultureInfo.InvariantCulture);
}