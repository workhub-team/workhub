using System.Text.Json.Serialization;

public class UnityDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }
}