using Microsoft.EntityFrameworkCore;
using UserPromo.Models;

namespace UserPromo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}
