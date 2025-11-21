using Microsoft.EntityFrameworkCore;

public class WorkHubContext : DbContext
{
    public WorkHubContext(DbContextOptions<WorkHubContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}