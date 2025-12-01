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
        return _context.Unities.Find(id);
    }

    public Unity GetUnityByName(string name)
    {
        Unity foundUnity = _context.Unities.FirstOrDefault(u => u.Name == name);
        return foundUnity;
    }

    public List<Unity> GetAllUnities()
    {
        return _context.Unities.ToList();
    }

    public string CreateUnity(UnityDto unityDto)
    {
        bool UnityExists = _context.Unities.Any(u => u.Name == unityDto.Name);

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
        var existingUnity = _context.Unities.Find(unityDto.Id);
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
        var unity = _context.Unities.Find(id);
        if (unity == null)
        {
            throw new Exception("Unity not found");
        }

        unity.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }
}