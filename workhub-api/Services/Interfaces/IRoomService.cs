public interface IRoomService
{
    public Room GetRoomById(string id);
    public List<Room> GetAllRooms();
    public Room CreateRoom(Room room);
    public Room UpdateRoom(Room room);
    public void DeleteRoom(string id);
}