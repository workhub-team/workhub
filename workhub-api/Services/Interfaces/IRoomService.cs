using Microsoft.AspNetCore.Mvc;

public interface IRoomService
{
    public DynamicResponse GetAllRoomsByUnityId(string unityId);
    public IActionResult CreateRoom(RoomDto roomDto);
    public DynamicResponse UpdateRoom(RoomDto roomDto);
    public IActionResult DeleteRoom(string id);
}