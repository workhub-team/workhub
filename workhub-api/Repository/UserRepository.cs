using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

public class UserRepository : IUserRepository
{
    private readonly WorkHubContext _context;

    public UserRepository(WorkHubContext context)
    {
        _context = context;
    }

    public bool CheckIfExistsByEmail(string email)
    {    
        return _context.Users.Any(u => u.Email == email);
    }

    public string RegisterNewUser(RegisterRequestDto newUser)
    {
        var user = new User
        {
            CompleteName = newUser.Username,
            Email = newUser.Email,
            Password = newUser.Password,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user.Id.ToString();
    }

    public bool LoginUser(LoginRequestDto user)
    {
        return _context.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
    }

    public string GenerateJwtToken(LoginRequestDto user)
    {
        //get user by email
        User foundUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        
        // Claims fofinhas UwU
        var claims = new[] {
            new Claim(ClaimTypes.Email, foundUser.Email),
            new Claim(ClaimTypes.Name, foundUser.CompleteName),
            new Claim(ClaimTypes.Role, foundUser.Role)
        };

        // Chave secreta peludinha
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("segredo-super-fofinho"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Criando o token UwU
        var token = new JwtSecurityToken(
            issuer: "workhub.com.br",
            audience: "workhub.com.br",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Sessão dura 30 min OwO
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);


    }
}