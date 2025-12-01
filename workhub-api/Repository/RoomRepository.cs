using Microsoft.AspNetCore.Http.HttpResults;

public class RoomRepository : IRoomRepository
{
    private readonly WorkHubContext _context;

    public RoomRepository(WorkHubContext context)
    {
        _context = context;
    }

    public Room GetRoomById(string id)
    {
        return _context.Rooms.Find(id);
    }

    public Room GetRoomByName(string name)
    {
        Room foundRoom = _context.Rooms.FirstOrDefault(u => u.Name == name);
        return foundRoom;
    }

    public List<Room> GetAllRooms()
    {
        return _context.Rooms.ToList();
    }

    public string CreateRoom(RoomDto roomDto)
    {
        bool RoomExists = _context.Rooms.Any(u => u.Name == roomDto.Name);

        Room room = new Room
        {
            Id = Guid.NewGuid().ToString(),
            UnityId = roomDto.UnityId,
            Name = roomDto.Name,
            Seats = roomDto.Seats,
            IsShared = roomDto.IsShared,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();
        return room.Id;
    }

    public Room UpdateRoom(RoomDto roomDto)
    {
        var existingRoom = _context.Rooms.Find(roomDto.RoomId);
        if (existingRoom == null)
        {
            throw new Exception("Room not found");
        }

        existingRoom.Name = roomDto.Name;
        existingRoom.Seats = roomDto.Seats;
        existingRoom.IsShared = roomDto.IsShared;
        existingRoom.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        return existingRoom;
    }

    public void DeleteRoom(string id)
    {
        var room = _context.Rooms.Find(id);
        if (room == null)
        {
            throw new Exception("Room not found");
        }

        room.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }
}