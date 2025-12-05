using System.Text.Json.Serialization;

public class UserReservesDto
{
    [JsonPropertyName("reserve_date")]
    public DateTime? ReserveDate { get; set; }

    [JsonPropertyName("reserve_period")]
    public string? ReservePeriod { get; set; }

    [JsonPropertyName("room_name")]
    public string? RoomName { get; set; }

    [JsonPropertyName("access_code")]
    public string? AccessCode { get; set; }
}