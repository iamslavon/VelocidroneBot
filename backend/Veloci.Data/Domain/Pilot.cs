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
}
