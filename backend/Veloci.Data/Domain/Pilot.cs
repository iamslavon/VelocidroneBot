using System.ComponentModel.DataAnnotations;

namespace Veloci.Data.Domain;

public class Pilot
{
    public Pilot()
    {
    }

    public Pilot(string name)
    {
        Name = name;
    }

    [Key]
    [MaxLength(128)]
    public string Name { get; set; }
    public DateTime? LastRaceDate { get; set; }
    public int DayStreak { get; set; }
    public int MaxDayStreak { get; set; }

    public void IncreaseDayStreak()
    {
        DayStreak++;

        if (DayStreak > MaxDayStreak)
            MaxDayStreak = DayStreak;
    }

    public void ResetDayStreak()
    {
        DayStreak = 0;
    }
}

public static class PilotExtensions
{
    /// <summary>
    /// Resets the day streaks of all pilots except the ones in the exceptPilots list.
    /// </summary>
    public static void ResetDayStreaksExcept(this IQueryable<Pilot> allPilots, List<string> exceptPilots)
    {
        allPilots
            .Where(p => !exceptPilots.Contains(p.Name))
            .ToList()
            .ForEach(p => p.ResetDayStreak());
    }
}
