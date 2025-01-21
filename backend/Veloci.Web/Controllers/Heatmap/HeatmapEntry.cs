namespace Veloci.Web.Controllers.Heatmap;

public class HeatmapEntry
{
    /// <summary>
    /// Date, when points were earned.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Earned points.
    /// </summary>
    public int Points { get; set; }
}
