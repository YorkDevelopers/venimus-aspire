using Microsoft.EntityFrameworkCore;
using venimus2.ApiService.Groups;

namespace venimus2.ApiService.Database;

public class VenimusDbContext(DbContextOptions<VenimusDbContext> options) : DbContext(options)
{
    public DbSet<Group> Groups { get; set; }
}