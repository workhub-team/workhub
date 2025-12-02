using System.Text.Json.Serialization;

public class LoginResponseDto
{
    [JsonPropertyName("jwt_token")]
    public string JwtToken { get; set; }

    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("user_role")]
    public string UserRole { get; set; }
}