namespace Veloci.Logic.Domain;

public class TrackGroup
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string Name { get; set; }

    public virtual IList<Track> Tracks { get; set; } = new List<Track>();
}