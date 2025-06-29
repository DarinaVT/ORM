using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Models;
using Infrastructure;
namespace DataContext;

public class CarsDbContext : DbContext
{
    public DbSet<ElectricCar> ElectricCars { get; set; }
    public DbSet<ElectricUtility> ElectricUtilities { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<MakeModelYear> MakeModelYears { get; set; }
    public DbSet<ElectricVehicleUtility> ElectricVehicleUtilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Parameters.ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElectricVehicleUtility>()
    .HasKey(e => new { e.ElectricCarId, e.ElectricUtilityId });
        base.OnModelCreating(modelBuilder);
    }
    public override int SaveChanges()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<Base>())
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
    public void DeleteEntity<T>(T entity) where T : Base
    {
        Set<T>().Remove(entity);
    }
}
