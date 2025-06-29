using Models;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
namespace DataContext;

public class SalariesDbContext : DbContext
{
    public DbSet<DepartmentName> Departments { get; set; }
    public DbSet<DivisionName> Divisions { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GlobalParam.ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.DepartmentName)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentNameId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Division)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DivisionNameId);


        modelBuilder.Entity<DepartmentName>().HasQueryFilter(d => !d.IsDeleted);
        modelBuilder.Entity<DivisionName>().HasQueryFilter(d => !d.IsDeleted);
        modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override int SaveChanges()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
                entry.Entity.UpdatedAt = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = utcNow;
            }
        }

        return base.SaveChanges();
    }
    public void DeleteEntity<T>(T entity) where T : BaseEntity
    {
        Set<T>().Remove(entity);
    }
}