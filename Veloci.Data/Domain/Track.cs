using System.ComponentModel.DataAnnotations.Schema;

namespace Veloci.Data.Domain;

public class Track
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int TrackId { get; set; }

    public string Name { get; set; }

    public virtual TrackMap Map { get; set; }
    public string MapId { get; set; }

    public double? Rating { get; set; }

    [NotMapped]
    public string FullName => $"{Map.Name} - {Name}";
}
