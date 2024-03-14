using EmployeeManagement.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Context;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasMany(x => x.Employees)
            .WithOne(y => y.DepartmentName)
            .HasForeignKey(x => x.DepartmentId)
            .IsRequired();


        base.OnModelCreating(modelBuilder);
    }
}
