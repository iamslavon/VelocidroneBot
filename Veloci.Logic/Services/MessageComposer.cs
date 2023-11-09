using System.Globalization;
using Veloci.Logic.Domain;

namespace Veloci.Logic.Services;

public class MessageComposer
{
    public string TimeUpdateMessage(IEnumerable<TrackTimeDelta> deltas)
    {
        var messages = deltas.Select(TimeUpdateMessage);
        return string.Join($"{Environment.NewLine}", messages);
    }

    private string TimeUpdateMessage(TrackTimeDelta delta) =>
        $"{delta.PlayerName} - {MsToSec(delta.TrackTime)}s ({MsToSec(delta.TimeChange)}s) / #{delta.Rank} (#{delta.RankOld})";
    private string MsToSec(int ms) => (ms / 1000.0).ToString(CultureInfo.InvariantCulture);
}