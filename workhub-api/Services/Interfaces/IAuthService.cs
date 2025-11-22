using Microsoft.AspNetCore.Mvc;

public interface IAuthService
{
    // bool VerifyPassword(string hashedPassword, string providedPassword);
    // string GenerateJwtToken(string userId, string email, string role);
    IActionResult RegisterUser(RegisterRequestDto userData);
    IActionResult LoginUser(LoginRequestDto userData);
}