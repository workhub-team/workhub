public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
    string GenerateJwtToken(string userId, string email, string role);
}