using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ConsoleApp3.Classes;


namespace AppDbCntxt;

class ApplicationDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<GuestHotel> GuestHotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-ENRVS12;Database=HotelManagement;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
