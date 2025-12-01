using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace workhub_api.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;    
            _configuration = configuration;
        }

        public IActionResult RegisterUser (RegisterRequestDto userData)
        {
            //primeiro checo se o email é válido
            //depois, se o email já ta sendo usado uwu
            bool userAlreadyExists = _userRepository.CheckIfExistsByEmail(userData.Email);

            //se tiver, acuso o golpe
            if (userAlreadyExists)
            {
                return new ObjectResult("Email já utilizado.") { StatusCode = 409 };    
            }
            
            //se n tiver, registro o novo carinha
            string newUserId = _userRepository.RegisterNewUser(userData);

            return new ObjectResult(newUserId) { StatusCode = 200 };
        }

        public IActionResult LoginUser (LoginRequestDto userData)
        {
            //checa se o usuario é cadastrado
            bool userAlreadyExists = _userRepository.CheckIfExistsByEmail(userData.Email);

            //se não tiver, acuso o golpe
            if (!userAlreadyExists)
            {
                return new ObjectResult("Usuário não foi encontrado.") { StatusCode = 404 };    
            }

            //se tiver, tento fazer login
            bool loginSuccesfull = _userRepository.LoginUser(userData);

            //se a senha não bater, acuso o golpe dnv
            if (!loginSuccesfull)
            {
                return new ObjectResult("Senha incorreta.") { StatusCode = 401 };    
            }

            //caso contrário, gerar jwt token
            string jwtToken = GenerateJwtToken(userData);

            return new ObjectResult(jwtToken) { StatusCode = 200 };;
        }

        public string GenerateJwtToken(LoginRequestDto user)
        {   
            //get user by email
            // User foundUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            User foundUser = _userRepository.GetUserByEmail(user.Email);
            
            // Claims fofinhas UwU
            var claims = new[] {
                new Claim("email", foundUser.Email),
                new Claim("name", foundUser.CompleteName),
                new Claim("role", foundUser.Role)
            };

            // Chave secreta peludinha
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Criando o token UwU
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Sessão dura 30 min OwO
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}