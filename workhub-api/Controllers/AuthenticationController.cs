using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using workhub_api.Services;

namespace workhub_api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController (IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            return _authService.LoginUser(request);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            return _authService.RegisterUser(request);
            // return StatusCode(200, "Ok");
        }
    }    
}

