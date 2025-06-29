using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CarCSV;

public class ApplicationDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CarDetails> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-ENRVS12;Database=CarDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
            sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .HasMany(b => b.Cars)
            .WithOne(c => c.Brand)
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<CarDetails>()
            .Property(c => c.Mpg)
            .HasPrecision(5, 2);

        modelBuilder.Entity<CarDetails>()
            .Property(c => c.Displacement)
            .HasPrecision(6, 2);

        modelBuilder.Entity<CarDetails>()
            .Property(c => c.Acceleration)
            .HasPrecision(5, 2);
    }
    public IQueryable<CarDetails> GetCarsByBrandStartingWith(char startsWith)
    {
        return Cars.Include(c => c.Brand).Where(c => c.Brand.Name.StartsWith(startsWith.ToString()));
    }

    public IQueryable<CarDetails> GetCarsProducedAfter(int year)
    {
        return Cars.Where(c => c.Year > year);
    }

    public IQueryable<CarDetails> GetCarsWithCylinders(int cylinders)
    {
        return Cars.Where(c => c.Cylinders == cylinders);
    }
}
