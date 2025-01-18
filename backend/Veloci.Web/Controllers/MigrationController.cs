using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;

namespace Veloci.Web.Controllers;

[ApiController]
[Route("/api/migration/[action]")]
public class MigrationController
{
    private readonly IRepository<Competition> _competitions;
    private readonly IRepository<Pilot> _pilots;

    public MigrationController(IRepository<Competition> competitions, IRepository<Pilot> pilots)
    {
        _competitions = competitions;
        _pilots = pilots;
    }

    [HttpGet("/api/migration/pilots")]
    public async Task Pilots()
    {
        if (await _pilots.GetAll().AnyAsync()) return;

        var allPilots = await _competitions.GetAll()
            .NotCancelled()
            .SelectMany(comp => comp.CompetitionResults)
            .Select(res => res.PlayerName)
            .Distinct()
            .Select(name => new Pilot(name))
            .ToListAsync();

        var competitions = _competitions
            .GetAll()
            .NotCancelled()
            .OrderByDescending(c => c.StartedOn);

        foreach (var pilot in allPilots)
        {
            pilot.LastRaceDate = competitions
                .FirstOrDefault(c => c.CompetitionResults.Any(r => r.PlayerName == pilot.Name))?
                .StartedOn;
        }

        var closedCompetitions = await competitions.Where(c => c.State == CompetitionState.Closed).ToListAsync();
        var lastClosedCompetition = closedCompetitions.FirstOrDefault();

        if (lastClosedCompetition is null)
            throw new Exception("What do you mean no last competition?");

        foreach (var result in lastClosedCompetition.CompetitionResults)
        {
            var dayStreak = closedCompetitions
                .TakeWhile(competition => competition.CompetitionResults.Any(r => r.PlayerName == result.PlayerName))
                .Count();

            var pilot = allPilots.FirstOrDefault(p => p.Name == result.PlayerName);

            if (pilot is not null)
                pilot.DayStreak = dayStreak;
        }

        await _pilots.AddRangeAsync(allPilots);
    }
}
