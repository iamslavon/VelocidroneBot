using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// The day when the pilot last raced.
    /// </summary>
    public DateTime? LastRaceDate { get; set; }
    public int DayStreak { get; set; }
    public int MaxDayStreak { get; set; }
    public virtual ICollection<PilotAchievement> Achievements { get; set; }

    public void IncreaseDayStreak(DateTime today)
    {
        if (LastRaceDate.HasValue && LastRaceDate.Value.Date == today.Date)
            return;

        DayStreak++;

        if (DayStreak > MaxDayStreak)
            MaxDayStreak = DayStreak;

        LastRaceDate = today;
    }

    public void ResetDayStreak()
    {
        DayStreak = 0;
    }
}

public static class PilotExtensions
{
    public static async Task ResetDayStreaksAsync(this IQueryable<Pilot> allPilots, DateTime today)
    {
        await allPilots
            .Where(p => p.LastRaceDate < today)
            .ExecuteUpdateAsync(x => x.SetProperty(pilot => pilot.DayStreak, 0));
    }
}
