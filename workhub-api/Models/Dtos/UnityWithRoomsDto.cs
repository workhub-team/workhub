using System.Text.Json.Serialization;

public class UnityWithRoomsDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("rooms")]
    public List<Room> Rooms { get; set; }
}