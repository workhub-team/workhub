using Microsoft.AspNetCore.Http.HttpResults;

public class UnityRepository : IUnityRepository
{
    private readonly WorkHubContext _context;

    public UnityRepository(WorkHubContext context)
    {
        _context = context;
    }

    public Unity GetUnityById(string id)
    {
        return _context.Unities.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
    }

    public Unity GetUnityByName(string name)
    {
        Unity foundUnity = _context.Unities.FirstOrDefault(u => u.Name == name && u.DeletedAt == null);
        return foundUnity;
    }

    public List<Unity> GetAllUnities()
    {
        return _context.Unities.Where(u => u.DeletedAt == null).ToList();
    }

    public string CreateUnity(UnityDto unityDto)
    {
        bool UnityExists = _context.Unities.Any(u => u.Name == unityDto.Name && u.DeletedAt == null);

        if (UnityExists)
        {
            throw new Exception("Unity with the same name already exists");
        } 
        
        Unity unity = new Unity
        {
            Id = Guid.NewGuid().ToString(),
            Name = unityDto.Name,
            Address = unityDto.Address,
            CreatedAt = DateTime.UtcNow
        };

        _context.Unities.Add(unity);
        _context.SaveChanges();
        return unity.Id;
    }

    public Unity UpdateUnity(UnityDto unityDto)
    {
        var existingUnity = _context.Unities.FirstOrDefault(u => u.Id == unityDto.Id && u.DeletedAt == null);
        if (existingUnity == null)
        {
            throw new Exception("Unity not found");
        }

        existingUnity.Name = unityDto.Name;
        existingUnity.Address = unityDto.Address;
        existingUnity.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        return existingUnity;
    }

    public void DeleteUnity(string id)
    {
        var unity = _context.Unities.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
        if (unity == null)
        {
            throw new Exception("Unity not found");
        }

        unity.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public string GetUnityByRoomId(string roomId)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId && r.DeletedAt == null);
        if (room == null)
        {
            throw new Exception("Room not found");
        }

        var unity = _context.Unities.FirstOrDefault(u => u.Id == room.UnityId && u.DeletedAt == null);
        if (unity == null)
        {
            throw new Exception("Unity not found");
        }

        return unity.Name+" - "+unity.Address;
    }
}