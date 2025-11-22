using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

public class AuthService : IAuthService 
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;    
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

        string jwtToken = _userRepository.GenerateJwtToken(userData); 

        return new ObjectResult(jwtToken) { StatusCode = 200 };;
    }
}