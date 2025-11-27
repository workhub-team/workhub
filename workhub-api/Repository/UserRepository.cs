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
        return _context.Users.Any(u => u.Email == email && u.DeletedAt == null);
    }

    public string RegisterNewUser(RegisterRequestDto newUser)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            CompleteName = newUser.Username,
            Email = newUser.Email,
            Password = newUser.Password,
            Role = "user",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user.Id.ToString();
    }

    public bool LoginUser(LoginRequestDto user)
    {
        return _context.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
    }

    public User GetUserByEmail(string email)
    {
        User foundUser = _context.Users.FirstOrDefault(u => u.Email == email);
        return foundUser;
    }
}