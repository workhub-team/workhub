using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace workhub_api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            return StatusCode(200, "token goes here owo");
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {

            return StatusCode(200, "Ok");
        }
    }    
}

