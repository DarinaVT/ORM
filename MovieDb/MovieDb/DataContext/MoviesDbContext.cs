namespace DataContext;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Models;

public class MoviesDbContext : DbContext
{
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movies> Movies { get; set; }
    public DbSet<MovieGenre> MoviesGenres { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GlobalParams.ConnectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenre>().HasKey(e => new {e.MovieId, e.GenreId});
        modelBuilder.Entity<MovieGenre>().HasOne(m => m.Movie).WithMany(g => g.MovieGenres).HasForeignKey(m => m.MovieId);
        modelBuilder.Entity<MovieGenre>().HasOne(m => m.Genre).WithMany(g => g.MovieGenres).HasForeignKey(m => m.GenreId);
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
}
