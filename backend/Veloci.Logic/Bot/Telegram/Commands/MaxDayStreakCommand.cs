using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot.Telegram.Commands.Core;

namespace Veloci.Logic.Bot.Telegram.Commands;

public class MaxDayStreakCommand : ITelegramCommand
{
    private readonly IRepository<Pilot> _pilots;

    public MaxDayStreakCommand(IRepository<Pilot> pilots)
    {
        _pilots = pilots;
    }

    public string[] Keywords => ["/max-day-streak", "/mds"];

    public string Description => "`/max-day-streak {pilotName}` або `/mds {pilotName}` - Maximum day streak";

    public async Task<string> ExecuteAsync(string[]? parameters)
    {
        if (parameters is null || parameters.Length == 0)
            return "все добре, але не вистачає імені пілота";

        var pilotName = string.Join(' ', parameters);
        var pilot = await _pilots.FindAsync(pilotName);

        return pilot is null
            ? $"Не знаю такого пілота 😕"
            : $"Max day streak: {pilot.MaxDayStreak}";
    }

    public bool RemoveMessageAfterDelay => false;
}
