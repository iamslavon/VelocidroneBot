using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Competitions;

public class CompetitionModel
{
    public required string Id { get; set; }
    public DateTime StartedOn { get; set; }
    public CompetitionState State { get; set; }
    public required string TrackName { get; set; }
}

public class DashboardModel
{
    public required CompetitionModel Competition { get; set; }
    public required List<TrackTimeModel> Results { get; set; }
}

public class TrackTimeModel
{
    public required string PlayerName { get; set; }
    public int Time { get; set; }
    public int ModelId { get; set; }

    public int GlobalRank { get; set; }
    public int LocalRank { get; set; }
}
