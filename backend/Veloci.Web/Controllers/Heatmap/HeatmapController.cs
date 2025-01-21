using Microsoft.AspNetCore.Mvc;

namespace Veloci.Web.Controllers.Heatmap;

[ApiController]
[Route("/api/heatmap/[action]")]
public class HeatmapController : ControllerBase
{
    private readonly HeatMapCalculator _calculator;

    public HeatmapController(HeatMapCalculator calculator)
    {
        _calculator = calculator;
    }

    [HttpGet]
    public async Task<List<HeatmapEntry>> ForPilot([FromQuery]string pilotName)
    {
        var data = await _calculator.GetHeatMap(pilotName);
        return data;
    }
}
