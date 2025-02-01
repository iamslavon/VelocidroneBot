namespace Veloci.Web.Controllers.Heatmap;

public class PilotResult
{
    /// <summary>
    /// Date, when points were earned.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Earned points.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Track time in milliseconds
    /// </summary>
    public int TrackTime { get; set; }
}
