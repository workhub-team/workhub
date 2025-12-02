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
        return _context.Rooms.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
    }

    public Room GetRoomByName(string name)
    {
        Room foundRoom = _context.Rooms.FirstOrDefault(u => u.Name == name && u.DeletedAt == null);
        return foundRoom;
    }

    public List<Room> GetAllRoomsByUnityId(string unityId)
    {
        return _context.Rooms.Where(r => r.UnityId == unityId && r.DeletedAt == null).ToList();
    }

    public string CreateRoom(RoomDto roomDto)
    {
        bool RoomExists = _context.Rooms.Any(u => u.Name == roomDto.Name);
        if (RoomExists)
        {
            throw new Exception("Room with the same name already exists");
        }

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
        var existingRoom = _context.Rooms.FirstOrDefault(u => u.Id == roomDto.RoomId && u.DeletedAt == null);
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
        var room = _context.Rooms.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
        if (room == null)
        {
            throw new Exception("Room not found");
        }

        room.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }
}