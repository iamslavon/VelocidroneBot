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

public static class IQueryableCompetionExtensions
{
    /// <summary>
    /// Filters competitions by date range
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="from">From date inclusive</param>
    /// <param name="to">To date exclusive</param>
    /// <returns></returns>
    public static IQueryable<Competition> InRange(this IQueryable<Competition> query, DateTime from, DateTime to)
    {
        return query.Where(comp => comp.StartedOn >= from && comp.StartedOn < to);
    }


    /// <summary>
    /// Filters competitions that are not cancelled
    /// </summary>
    /// <param name="query">Source query</param>
    /// <returns></returns>
    public static IQueryable<Competition> NotCancelled(this IQueryable<Competition> query)
    {
        return query.Where(comp => comp.State != CompetitionState.Cancelled);
    }
}
