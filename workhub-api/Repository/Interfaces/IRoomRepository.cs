public interface IRoomRepository
{
    Room GetRoomById(string id);
    Room GetRoomByName(string name);
    List<Room> GetAllRooms();
    string CreateRoom(RoomDto roomDto);
    Room UpdateRoom(RoomDto roomDto);
    void DeleteRoom(string id);
}