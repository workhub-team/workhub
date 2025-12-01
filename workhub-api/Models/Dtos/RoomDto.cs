using System.Text.Json.Serialization;

public class RoomDto
{
    [JsonPropertyName("unity_id")]
    public string? UnityId { get; set; }

    [JsonPropertyName("room_id")]
    public string? RoomId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }    

    [JsonPropertyName("seats")]
    public int Seats { get; set; }

    [JsonPropertyName("is_shared")]
    public bool IsShared { get; set; }
}