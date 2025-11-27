using Microsoft.EntityFrameworkCore;

public class WorkHubContext : DbContext
{
    public WorkHubContext(DbContextOptions<WorkHubContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Local> Locals { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Allocation> Allocations { get; set; }
}