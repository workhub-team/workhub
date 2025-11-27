using Microsoft.EntityFrameworkCore;

public class WorkHubContext : DbContext
{
    public WorkHubContext(DbContextOptions<WorkHubContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Unity> Unities { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reserve> Reserves { get; set; }
}