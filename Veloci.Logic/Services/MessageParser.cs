using System.Text.RegularExpressions;

namespace Veloci.Logic.Services;

public static class MessageParser
{
    public static bool IsStartCompetition(string message) => message.Contains("Ласкаво просимо");

    public static bool IsStopCompetition(string message) => message.Contains("Результати дня");

    public static bool IsTempSeasonResults(string message) => message.Contains("Проміжні результати місяця");

    public static bool IsStopSeason(string message) => message.Contains("#velocibotmonthlyfinal");

    public static (string map, string track) GetTrackName(string text)
    {
        var lines = text.Split('\n');

        if (lines.Length < 2)
            throw new Exception("Could not get track name from the message");

        var fullName = lines[1];
        var fullNameSplit = fullName.Split(" - ");

        return (map: fullNameSplit[0], track: fullNameSplit[1]);
    }

    public static int GetTrackId(string text)
    {
        const string pattern = @"https://www.velocidrone.com/leaderboard/(\d+)/(\d+)/All";
        var match = Regex.Match(text, pattern);

        if (match.Success)
        {
            var idString = match.Groups[2].Value;

            if (int.TryParse(idString, out var id))
            {
                return id;
            }
        }

        throw new Exception("Track id not found");
    }
}
