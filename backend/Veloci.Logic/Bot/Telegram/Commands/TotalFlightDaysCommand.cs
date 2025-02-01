using Microsoft.EntityFrameworkCore;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Bot.Telegram.Commands.Core;

namespace Veloci.Logic.Bot.Telegram.Commands;

public class TotalFlightDaysCommand : ITelegramCommand
{
    private readonly IRepository<Competition> _competitions;

    public TotalFlightDaysCommand(IRepository<Competition> competitions)
    {
        _competitions = competitions;
    }

    public string[] Keywords => ["/total-flight-days"];
    public string Description => "`/total-flight-days {pilotName}` - Total flight days";
    public async Task<string> ExecuteAsync(string[]? parameters)
    {
        if (parameters is null || parameters.Length == 0)
            return "все добре, але не вистачає імені пілота";

        if (parameters.Length > 1)
            return "все добре, але забагато параметрів";

        var count = await _competitions
            .GetAll()
            .NotCancelled()
            .Where(comp => comp.CompetitionResults.Any(res => res.PlayerName == parameters[0]))
            .CountAsync();

        return $"Загальна кількість днів: {count}";
    }

    public bool RemoveMessageAfterDelay => false;
}
