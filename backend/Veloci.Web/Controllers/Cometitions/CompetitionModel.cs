using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Cometitions;

public class CompetitionModel
{
    public string Id { get; set; }
    public DateTime StartedOn { get; set; }
    public CompetitionState State { get; set; }
    public string TrackName { get; set; }
}

public class DashboardModel
{
    public CompetitionModel Competition { get; set; }
    public List<TrackTimeModel> Results { get; set; }
}

public class TrackTimeModel
{
    public string PlayerName { get; set; }
    public int Time { get; set; }
    public int ModelId { get; set; }

    public int GlobalRank { get; set; }
    public int LocalRank { get; set; }
}
