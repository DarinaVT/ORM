using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Company.CompanyManagement;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskList> Tasks { get; set; }
    public DbSet<AssignedTask> AssignedTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-ENRVS12;Database=UserToDo;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
