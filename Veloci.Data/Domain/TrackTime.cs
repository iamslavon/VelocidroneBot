namespace Veloci.Data.Domain;

public class TrackTime
{
    public TrackTime()
    {
    }

    public TrackTime(int globalRank, string name, int time)
    {
        GlobalRank = globalRank;
        PlayerName = name;
        Time = time;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Time { get; set; }
    public string PlayerName { get; set; }
    public int ModelId { get; set; }
    
    public int GlobalRank { get; set; }
    public int LocalRank { get; set; }
}