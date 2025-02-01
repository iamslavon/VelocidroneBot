using Microsoft.EntityFrameworkCore;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;

namespace Veloci.Web.Controllers.Heatmap;

public class PilotResultsCalculator
{
    private readonly IRepository<CompetitionResults> _competitionResults;

    public PilotResultsCalculator(IRepository<CompetitionResults> competitionResults)
    {
        _competitionResults = competitionResults;
    }

    public Task<List<PilotResult>> GetPilotResults(string pilotName)
    {
        var now = DateTime.Today;
        var start = now.AddYears(-1);

        var data = _competitionResults
            .GetAll()
            .Where(c => c.Competition.StartedOn >= start)
            .Where(c => c.PlayerName == pilotName && c.Competition.State == CompetitionState.Closed)
            .OrderBy(x => x.Competition.StartedOn)
            .ProjectToModel()
            .ToListAsync();
        return data;
    }
}
