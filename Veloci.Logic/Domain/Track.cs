using System.ComponentModel.DataAnnotations.Schema;

namespace Veloci.Logic.Domain;

public class Track
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public int TrackId { get; set; }
    
    public string Name { get; set; }
    
    public virtual TrackGroup Group { get; set; }
    public string GroupId { get; set; }

    [NotMapped]
    public string FullName => $"{Group.Name} - {Name}";
}