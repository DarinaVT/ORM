namespace DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Estate.Models;
public class PropertiesDbContext : DbContext
{
    public DbSet<PropertyInfo> PropertiesInfo { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<Remark> Remarks { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-ENRVS12;Database=RealEstateDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyInfo>().HasOne(p => p.Location).WithOne(l => l.Property).HasForeignKey<Location>(l => l.PropertyId);
        modelBuilder.Entity<PropertyInfo>().HasOne(p => p.Assessment).WithOne(a => a.Property).HasForeignKey<Assessment>(a => a.PropertyId);
        modelBuilder.Entity<PropertyInfo>().HasOne(p => p.Remark).WithOne(r => r.Property).HasForeignKey<Remark>(r => r.PropertyId);
        base.OnModelCreating(modelBuilder);
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
    //hard delete entity
    public void DeleteEntity<T>(T entity) where T : BaseEntity
    {
        Set<T>().Remove(entity);
    }
}