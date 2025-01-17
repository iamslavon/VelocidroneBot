namespace Veloci.Data.Domain;

public class TrackRating
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int PollMessageId { get; set; }

    public double? Value { get; set; }
}
