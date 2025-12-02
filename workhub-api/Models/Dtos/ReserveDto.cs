using System.Text.Json.Serialization;

public class ReserveDto
{
    [JsonPropertyName("reserve_id")]
    public string? ReserveId { get; set; }

     [JsonPropertyName("room_id")]
    public string RoomId { get; set; }

     [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("reserved_day")]
    public DateTime ReservedDay { get; set; }

    [JsonPropertyName("reserved_period")]
    public string ReservedPeriod { get; set; }

    [JsonPropertyName("entry_code")]
    public string? EntryCode { get; set; }
}