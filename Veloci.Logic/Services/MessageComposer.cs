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

    public string TempLeaderboard(IEnumerable<CompetitionResults> results)
    {
        var rows = results.Select(TempLeaderboardRow);
        return $"🏆 Проміжні результати:{Environment.NewLine}{Environment.NewLine}{string.Join($"{Environment.NewLine}", rows)}";
    }

    public string Leaderboard(IEnumerable<CompetitionResults> results, string trackName)
    {
        var rows = results.Select(LeaderboardRow);
        return $"🏆 РЕЗУЛЬТАТИ ДНЯ{Environment.NewLine}" +
               $"Трек: *{trackName}*{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}", rows)}";
    }

    #region Private

    private string TimeUpdate(TrackTimeDelta delta)
    {
        var timeChangePart = delta.TimeChange.HasValue ? $" ({MsToSec(delta.TimeChange.Value)}s)" : string.Empty;
        var rankOldPart = delta.RankOld.HasValue ? $" (#{delta.RankOld})" : string.Empty;

        return $"⏱ *{delta.PlayerName}* - {MsToSec(delta.TrackTime)}s{timeChangePart} / #{delta.Rank}{rankOldPart}";
    }

    private string TempLeaderboardRow(CompetitionResults time)
    {
        return $"{time.LocalRank} - *{time.PlayerName}* ({MsToSec(time.TrackTime)}s)";
    }

    private string LeaderboardRow(CompetitionResults time)
    {
        var icon = time.LocalRank switch
        {
            1 => "🥇",
            2 => "🥈",
            3 => "🥉",
            _ => $"{time.LocalRank}"
        };

        return $"{icon} - *{time.PlayerName}* ({MsToSec(time.TrackTime)}s) / Балів: *{time.Points}*";
    }

    private static string MsToSec(int ms) => (ms / 1000.0).ToString(CultureInfo.InvariantCulture);

    #endregion
}
