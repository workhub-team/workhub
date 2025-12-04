using System.Text.Json.Serialization;

public class VerifyResponseDto
{
    [JsonPropertyName("is_available")]
    public bool IsAvailable { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("requested_date")]
    public DateTime RequestedDate { get; set; }

    [JsonPropertyName("requested_period")]
    public string RequestedPeriod { get; set; }
}