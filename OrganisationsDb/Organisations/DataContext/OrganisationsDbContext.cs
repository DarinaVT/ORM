namespace DataContext;
using Infrastructure;
using Models;
using Microsoft.EntityFrameworkCore;

public class OrganisationsDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Year> Years { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GlobalParams.ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organisation>().HasKey(i => i.Index);
        modelBuilder.Entity<Organisation>().ToTable("organisations");
        modelBuilder.Entity<Country>().ToTable("countries");
        modelBuilder.Entity<Industry>().ToTable("industries");
        modelBuilder.Entity<Year>().ToTable("years");
        modelBuilder.Entity<Organisation>().HasOne(o => o.Country).WithMany(o => o.Organisations).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Organisation>().HasOne(o => o.Industry).WithMany(o => o.Organisations).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Organisation>().HasOne(o => o.Year).WithMany(o => o.Organisations).OnDelete(DeleteBehavior.Restrict);
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
