public class UnityService : IUnityService
{

    private readonly IUnityRepository _unityRepository;

    public UnityService(WorkHubContext context)
    {
        _context = context;
    }

    public Unity GetUnityById(string id)
    {
        return _context.Unities.Find(id);
    }

    public List<Unity> GetAllUnities()
    {
        return _context.Unities.ToList();
    }

    public IResult CreateUnity(UnityDto unityDto)
    {
        bool UnityExists = _context;

        if (UnityExists)
        {
            return Results.Conflict("A unidadde com este nome já existe.");
        };

        _context.Unities.Add(unity);
        _context.SaveChanges();
        return unity;
    }

    public Unity UpdateUnity(Unity unity)
    {
        var existingUnity = _context.Unities.Find(unity.Id);
        if (existingUnity == null)
        {
            throw new Exception("Unity not found");
        }

        existingUnity.Name = unity.Name;
        existingUnity.Address = unity.Address;
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