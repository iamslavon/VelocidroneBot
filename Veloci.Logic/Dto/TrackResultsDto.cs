using System.Text.Json.Serialization;

namespace Veloci.Logic.Dto;

public class TrackResultsDto
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message_title")]
    public string MessageTitle { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("tracktimes")]
    public List<TrackTimeDto>? Tracktimes { get; set; }
}