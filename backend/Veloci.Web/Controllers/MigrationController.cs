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

        var competitions = await _competitions
            .GetAll()
            .OrderByDescending(c => c.StartedOn)
            .Where(c => c.State == CompetitionState.Closed)
            .ToListAsync();

        foreach (var pilot in allPilots)
        {
            pilot.LastRaceDate = competitions
                .FirstOrDefault(c => c.CompetitionResults.Any(r => r.PlayerName == pilot.Name))?
                .StartedOn;

            pilot.MaxDayStreak = GetMaxDayStreak(pilot.Name, competitions);
        }

        var lastCompetition = competitions.FirstOrDefault();

        if (lastCompetition is null)
            throw new Exception("What do you mean no last competition?");

        foreach (var result in lastCompetition.CompetitionResults)
        {
            var dayStreak = competitions
                .TakeWhile(competition => competition.CompetitionResults.Any(r => r.PlayerName == result.PlayerName))
                .Count();

            var pilot = allPilots.FirstOrDefault(p => p.Name == result.PlayerName);

            if (pilot is not null)
                pilot.DayStreak = dayStreak;
        }

        await _pilots.AddRangeAsync(allPilots);
    }

    private int GetMaxDayStreak(string pilot, List<Competition> competitions)
    {
        var maxStreak = 0;
        var currentStreak = 0;
        DateTime? lastDate = null;

        var pilotsCompetitions = competitions
            .Where(competition => competition.CompetitionResults.Any(r => r.PlayerName == pilot));

        foreach (var competition in pilotsCompetitions)
        {
            if (lastDate.HasValue && (lastDate.Value - competition.StartedOn).Days == 1)
            {
                currentStreak++;
            }
            else
            {
                currentStreak = 1;
            }

            maxStreak = Math.Max(maxStreak, currentStreak);
            lastDate = competition.StartedOn;
        }

        return maxStreak;
    }
}
