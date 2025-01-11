using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veloci.Logic.Services;

namespace Veloci.Web.Controllers.Competitions;

[ApiController]
[Route("/api/competitions/[action]")]
public class CompetitionsController : ControllerBase
{
    private readonly CompetitionService _competitionService;

    public CompetitionsController(CompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    [HttpGet("/api/competitions/current")]
    public async Task<CompetitionModel[]> GetCurrent()
    {
        var competitions  = await _competitionService.GetCurrentCompetitions().ProjectToModel().ToArrayAsync() ;
        return competitions;
    }

    [HttpGet("/api/dashboard")]
    public async Task<DashboardModel?> Dashboard()
    {
        var competition  = await _competitionService.GetCurrentCompetitions().FirstOrDefaultAsync();

        if (competition is null) return null;

        var dashboardModel = new DashboardModel
        {
            Competition = competition.MapToModel(),
            Results = competition.CurrentResults
                .Times
                .OrderBy(x => x.Time)
                .MapToModel()
        };

        return dashboardModel;
    }
}
