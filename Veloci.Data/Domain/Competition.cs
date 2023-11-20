namespace Veloci.Data.Domain;

public class Competition
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime StartedOn { get; set; } = DateTime.Now;

    public CompetitionState State { get; set; }

    public long ChatId { get; set; }

    public virtual Track Track { get; set; }
    public string TrackId { get; set; }

    public virtual TrackResults InitialResults { get; set; }

    public virtual TrackResults CurrentResults { get; set; }

    public virtual List<TrackTimeDelta> TimeDeltas { get; set; }

    public virtual List<CompetitionResults> CompetitionResults { get; set; }

    public bool ResultsPosted { get; set; }
}
