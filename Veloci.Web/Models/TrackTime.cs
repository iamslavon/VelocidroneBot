using System.Text.Json.Serialization;

namespace Veloci.Web.Models;

public class TrackTime
{
    [JsonPropertyName("lap_time")]
    public string LapTime { get; set; }

    [JsonPropertyName("playername")]
    public string Playername { get; set; }

    [JsonPropertyName("model_id")]
    public int ModelId { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("sim_version")]
    public string SimVersion { get; set; }

    [JsonPropertyName("device_type")]
    public int DeviceType { get; set; }
}