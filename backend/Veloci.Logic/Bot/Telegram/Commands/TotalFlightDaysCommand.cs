using Microsoft.EntityFrameworkCore;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot.Telegram.Commands.Core;

namespace Veloci.Logic.Bot.Telegram.Commands;

public class TotalFlightDaysCommand : ITelegramCommand
{
    private readonly IRepository<Competition> _competitions;
    private readonly IRepository<Pilot> _pilots;

    public TotalFlightDaysCommand(IRepository<Competition> competitions, IRepository<Pilot> pilots)
    {
        _competitions = competitions;
        _pilots = pilots;
    }

    public string[] Keywords => ["/total-flight-days"];
    public string Description => "`/total-flight-days {pilotName}` - Total flight days";
    public async Task<string> ExecuteAsync(string[]? parameters)
    {
        if (parameters is null || parameters.Length == 0)
            return "все добре, але не вистачає імені пілота";

        var pilotName = string.Join(' ', parameters);
        var pilot = await _pilots.FindAsync(pilotName);

        if (pilot is null)
            return $"Не знаю такого пілота 😕";

        var count = await _competitions
            .GetAll()
            .NotCancelled()
            .Where(comp => comp.CompetitionResults.Any(res => res.PlayerName == pilotName))
            .CountAsync();

        return $"Загальна кількість днів: {count}";
    }

    public bool RemoveMessageAfterDelay => false;
}
