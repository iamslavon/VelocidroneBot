namespace Veloci.Data.Domain;

public class TrackTimeDelta
{
    public TrackTimeDelta()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public virtual Competition Competition { get; set; }
    public string CompetitionId { get; set; }
    public string PlayerName { get; set; }
    public int TrackTime { get; set; }
    public int? TimeChange { get; set; }
    public int Rank { get; set; }
    public int? RankOld { get; set; }
    public int LocalRank { get; set; }
    public int? LocalRankOld { get; set; }
    public int? DroneModelId { get; set; }
    public virtual DroneModel? DroneModel { get; set; }
    public int? UnknownDroneModelId { get; set; }
}
