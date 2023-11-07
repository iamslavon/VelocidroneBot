namespace Veloci.Logic.Domain;

public class Track
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public int TrackId { get; set; }
    
    public string Name { get; set; }
}