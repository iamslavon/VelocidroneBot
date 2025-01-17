namespace Veloci.Data.Domain;

public class Pilot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public DateTime? LastRaceDate { get; set; }
    public int DayStreak { get; set; }
}
