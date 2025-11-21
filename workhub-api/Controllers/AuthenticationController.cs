using Microsoft.AspNetCore.Mvc;

namespace workhub_api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody])
        {
            
        }
    }    
}

