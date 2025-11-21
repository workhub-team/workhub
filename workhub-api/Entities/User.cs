using Microsoft.EntityFrameworkCore;

public class User
{
    public string Id { get; set;}
    public string CompleteName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set;}
    public DateTime UpdatedAt { get; set;}
    public DateTime DeletedAt { get; set;}
}