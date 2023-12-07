using System.Globalization;
using System.Text;
using Veloci.Data.Domain;
using Veloci.Logic.Bot;

namespace Veloci.Logic.Services;

public class MessageComposer
{
    public string TimeUpdate(IEnumerable<TrackTimeDelta> deltas)
    {
        var messages = deltas.Select(TimeUpdate);
        return string.Join($"{Environment.NewLine}", messages);
    }

    public string StartCompetition(Track track)
    {
        return $"📅 Вітаємо на щоденному FPV онлайн-турнірі!{Environment.NewLine}{Environment.NewLine}" +
               $"Трек дня: *{track.FullName}*{Environment.NewLine}{Environment.NewLine}" +
               $"Leaderboard: *https://www.velocidrone.com/leaderboard/{track.Map.MapId}/{track.TrackId}/All*";
    }

    public BotPoll Poll(string trackName)
    {
        var question = $"Оцініть трек {trackName}{Environment.NewLine}{Environment.NewLine}" +
               $"Не забувайте оцінювати треки!";

        var options = new List<BotPollOption>
        {
            new (3, "Один із кращих"),
            new (1, "Подобається"),
            new (0, "Нормальний"),
            new (-1, "Не дуже"),
            new (-3, "Лайно")
        };

        return new BotPoll
        {
            Question = question,
            Options = options
        };
    }

    public string BadTrackRating()
    {
        return $"😔 Бачу трек не сподобався. Більше його не буде";
    }

    public string TempLeaderboard(IEnumerable<CompetitionResults> results)
    {
        var rows = results.Select(TempLeaderboardRow);
        return $"🧐 Проміжні результати:{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}", rows)}";
    }

    public string Leaderboard(IEnumerable<CompetitionResults> results, string trackName)
    {
        var rows = results.Select(LeaderboardRow);
        return $"🏆 РЕЗУЛЬТАТИ ДНЯ{Environment.NewLine}" +
               $"Трек: *{trackName}*{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}{Environment.NewLine}", rows)}";
    }

    public string TempSeasonResults(IEnumerable<SeasonResult> results)
    {
        var rows = results.Select(TempSeasonResultsRow);
        return $"🗓 Проміжні результати місяця{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}{Environment.NewLine}", rows)}";
    }

    public string SeasonResults(IEnumerable<SeasonResult> results)
    {
        var rows = results.Select(SeasonResultsRow);
        return $"🏁 Фінальні результати місяця{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}{Environment.NewLine}", rows)}";
    }

    public string MedalCount(IEnumerable<SeasonResult> results)
    {
        var rows = results.Select(MedalCountRow);
        return $"*Медалі за місяць*{Environment.NewLine}{Environment.NewLine}" +
               $"{string.Join($"{Environment.NewLine}{Environment.NewLine}", rows)}";
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
            _ => $"#{time.LocalRank}"
        };

        return $"{icon} - *{time.PlayerName}* ({MsToSec(time.TrackTime)}s) / Балів: *{time.Points}*";
    }

    private string TempSeasonResultsRow(SeasonResult result)
    {
        return $"{result.Rank} - *{result.PlayerName}* - {result.Points} балів";
    }

    private string SeasonResultsRow(SeasonResult result)
    {
        var icon = result.Rank switch
        {
            1 => "🥇",
            2 => "🥈",
            3 => "🥉",
            _ => $"#{result.Rank}"
        };

        return $"{icon} - *{result.PlayerName}* - {result.Points} балів";
    }

    private string? MedalCountRow(SeasonResult result)
    {
        if (result is { GoldenCount: 0, SilverCount: 0, BronzeCount: 0 })
            return null;

        var medals = $"{MedalsRow("🥇", result.GoldenCount)}{MedalsRow("🥈", result.SilverCount)}{MedalsRow("🥉", result.BronzeCount)}";
        return $"*{result.PlayerName}*:{Environment.NewLine}{medals}";
    }

    private string MedalsRow(string medalIcon, int count)
    {
        var result = new StringBuilder();

        for (var i = 0; i < count; i++)
        {
            result.Append(medalIcon);
        }

        return result.ToString();
    }

    private static string MsToSec(int ms) => (ms / 1000.0).ToString(CultureInfo.InvariantCulture);

    #endregion
}
