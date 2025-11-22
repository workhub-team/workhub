using Microsoft.AspNetCore.Mvc.Rendering;

public interface IUserRepository
{
    bool CheckIfExistsByEmail(string email);
    string RegisterNewUser(RegisterRequestDto newUser);
    bool LoginUser(LoginRequestDto user);
    string GenerateJwtToken(LoginRequestDto user);
} 