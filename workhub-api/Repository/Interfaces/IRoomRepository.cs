public interface IRoomRepository
{
    Room GetRoomById(string id);
    Room GetRoomByName(string name);
    List<Room> GetAllRoomsByUnityId(string unityId);
    string CreateRoom(RoomDto roomDto);
    Room UpdateRoom(RoomDto roomDto);
    void DeleteRoom(string id);
}