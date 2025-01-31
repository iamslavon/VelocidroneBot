using System.Text.RegularExpressions;

namespace Veloci.Logic.Services;

public static class MessageParser
{
    private static readonly Regex CompetitionRestartRegex =
        new("новий трек, будь ласка", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static bool IsCompetitionRestart(string message) => CompetitionRestartRegex.IsMatch(message);
    public static bool IsCurrentDayStreakCommand(string message) => message.StartsWith("/cds ");
    public static string GetCurrentDayStreakCommandParameter(string message) => message[5..].Trim();
}
