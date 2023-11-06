using System.Text.Json.Serialization;

namespace Veloci.Web.Models;

public class TrackResults
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message_title")]
    public string MessageTitle { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("tracktimes")]
    public List<TrackTime>? Tracktimes { get; set; }
}