using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using workhub_api.Services;

namespace workhub_api.Controllers
{
    [ApiController]
    [Route("reserve")]
    public class ReservationController : ControllerBase
    {
        private readonly IReserveService _reserveService;

        public ReservationController (IReserveService reserveService)
        {
            _reserveService = reserveService;
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateReserve([FromBody] ReserveDto request)
        {
            return _reserveService.CreateReserve(request);
        }

        [HttpPost("update")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateReserve([FromBody] ReserveDto request)
        {
            var result = _reserveService.UpdateReserve(request);
            return StatusCode(result.StatusCode , result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteRoom([FromRoute] string id)
        {
            return StatusCode(200, _reserveService.DeleteReserve(id));
        }

        [HttpGet("list/{userId}")]
        [Authorize]
        public IActionResult GetAllRoomsByUnity([FromRoute] string userId)
        {
            DynamicResponse reserves = _reserveService.GetAllReservesByUserId(userId);
            return Ok(reserves);
        }
    }    
}