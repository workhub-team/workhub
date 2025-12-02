using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using workhub_api.Services;

namespace workhub_api.Controllers
{
    // [ApiController]
    // [Route("room")]
    // public class ReservationController : ControllerBase
    // {
    //     private readonly IReservationService _reservationService;

    //     public ReservationController (IReservationService _reservationService)
    //     {
    //         _roomService = roomService;
    //     }

    //     [HttpPost("create")]
    //     [Authorize(Roles = "admin")]
    //     public IActionResult CreateRoom([FromBody] RoomDto request)
    //     {
    //         return _roomService.CreateRoom(request);
    //     }

    //     [HttpPost("update")]
    //     [Authorize(Roles = "admin")]
    //     public IActionResult UpdateRoom([FromBody] RoomDto request)
    //     {
    //         var result = _roomService.UpdateRoom(request);
    //         return StatusCode(result.StatusCode , result);
    //     }

    //     [HttpDelete("delete/{id}")]
    //     [Authorize(Roles = "admin")]
    //     public IActionResult DeleteRoom([FromRoute] string id)
    //     {
    //         return StatusCode(200, _roomService.DeleteRoom(id));
    //     }

    //     [HttpGet("list")]
    //     public IActionResult GetAllRoomsByUnity(string unityId)
    //     {
    //         DynamicResponse unities = _roomService.GetAllRoomsByUnityId(unityId);
    //         return Ok(unities);
    //     }
    // }    
}