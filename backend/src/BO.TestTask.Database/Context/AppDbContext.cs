using BO.TestTask.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BO.TestTask.Database.Context;

public sealed class AppDbContext : DbContext
{
    public DbSet<EmployeeEntity> Employees { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
