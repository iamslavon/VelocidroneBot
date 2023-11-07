﻿namespace Veloci.Logic.Domain;

public class Competition
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime StartedOn { get; set; } = DateTime.Now;
    public CompetitionState State { get; set; }
    public virtual Track Track { get; set; }
    public virtual TrackResults InitialResults { get; set; }
    public virtual TrackResults CurrentResults { get; set; }
    public virtual List<TrackTimeDelta> TimeDeltas { get; set; }
}