using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using workhub_api.Services;

namespace workhub_api.Controllers
{
    [ApiController]
    [Route("unity")]
    public class UnityController : ControllerBase
    {
        private readonly IUnityService _unityService;

        public UnityController (IUnityService unityService)
        {
            _unityService = unityService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateUnity([FromBody] UnityDto request)
        {
            return _unityService.CreateUnity(request);
        }

        [HttpPost("update")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateUnity([FromBody] UnityDto request)
        {
            var result = _unityService.UpdateUnity(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUnity([FromRoute] string id)
        {
            return StatusCode(200, _unityService.DeleteUnity(id));
        }

        [HttpGet("list")]
        public IActionResult GetAllUnities()
        {
            DynamicResponse unities = _unityService.GetAllUnities();
            return Ok(unities);
        }
    }    
}

