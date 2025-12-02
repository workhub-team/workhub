using System.CodeDom.Compiler;

public class ReserveRepository : IReserveRepository
{
    private readonly WorkHubContext _context;

    public ReserveRepository(WorkHubContext context)
    {
        _context = context;
    }

    public Reserve GetReserveById(string id)
    {
        return _context.Reserves.Find(id);
    }

    public List<Reserve> GetAllReservesByUserId(string userId)
    {
        return _context.Reserves.Where(r => r.UserId == userId).ToList();
    }

    public string CreateReserve(ReserveDto reserveDto)
    {
        var newReserve = new Reserve
        {
            Id = Guid.NewGuid().ToString(),
            UserId = reserveDto.UserId,
            RoomId = reserveDto.RoomId,
            ReservedDay = reserveDto.ReservedDay,
            ReservedPeriod = reserveDto.ReservedPeriod,
            AccessCode = GenerateAccessCode(),
            CreatedAt = DateTime.UtcNow,
        };


        _context.Reserves.Add(newReserve);
        _context.SaveChanges();

        return newReserve.Id;
    }

    public Reserve UpdateReserve(ReserveDto reserveDto)
    {
        var existingReserve = _context.Reserves.FirstOrDefault(u => u.Id == reserveDto.ReserveId && u.DeletedAt == null);

        if (existingReserve == null)
        {
            throw new Exception("Reserve not found");
        }

        existingReserve.RoomId = reserveDto.RoomId;
        existingReserve.ReservedDay = reserveDto.ReservedDay;
        existingReserve.ReservedPeriod = reserveDto.ReservedPeriod;
        existingReserve.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        return existingReserve;
    }

    public void DeleteReserve(string id)
    {
        var reserve = _context.Reserves.FirstOrDefault(r => r.Id == id && r.DeletedAt == null);
        if (reserve != null)
        {
            throw new Exception("Reserve not found");
        }

        reserve.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public bool ValidateReserve(ReserveDto reserveDto)
    {
        List<Reserve> conflictingReserves = _context.Reserves
            .Where(u => u.ReservedDay == reserveDto.ReservedDay && 
                (u.ReservedPeriod == reserveDto.ReservedPeriod || u.ReservedPeriod == "full")
            ).ToList();

        if (conflictingReserves.Count > 0)
        {
            return false;
        }
        return true;
    }

    private string GenerateAccessCode()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 6)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}