using Microsoft.EntityFrameworkCore;
using PositionManager.Domain.Entities;

namespace PositionManager.Persisctence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
             Database.EnsureCreated();
        }
        public DbSet<EmployeePosition> Positions { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
    }
}
