using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Cometitions;

public class CompetitionModel
{
    public string Id { get; set; }
    public DateTime StartedOn { get; set; }
    public CompetitionState State { get; set; }
    public string TrackName { get; set; }
}
