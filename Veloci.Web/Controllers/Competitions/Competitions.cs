using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veloci.Logic.Services;

namespace Veloci.Web.Controllers.Competitions;

[Route("/api/competitions/[action]")]
public class CompetitionsController : ControllerBase
{
    private readonly CompetitionService _competitionService;

    public CompetitionsController(CompetitionService competitionService)
    {
        _competitionService = competitionService;
    }
    [HttpGet("/api/competitions/current")]
    public async Task<IActionResult> GetCurrent()
    {
        var competitions  = await _competitionService.GetCurrentCompetitions().ProjectToModel().ToListAsync() ;
        return Ok(competitions);
    }
}
