using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Competitions;

public class CompetitionModel
{
    public string Id { get; set; }
    public DateTime StartedOn { get; set; }
    public CompetitionState State { get; set; }
    public string MapName { get; set; }
    public string TrackName { get; set; }
    public int TrackId { get; set; }
    public int MapId { get; set; }
}

public class DashboardModel
{
    public required CompetitionModel Competition { get; set; }
    public required List<TrackTimeModel> Results { get; set; }
    public required List<SeasonResultModel> Leaderboard { get; set; }
}

public class TrackTimeModel
{
    public required string PlayerName { get; set; }
    public int Time { get; set; }
    public int GlobalRank { get; set; }
    public int LocalRank { get; set; }
}

public class SeasonResultModel
{
    public required string PlayerName { get; set; }
    public int Points { get; set; }
}
