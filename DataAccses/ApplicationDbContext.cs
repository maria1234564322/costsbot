using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Outlay> Outlay { get; set; }
    public DbSet<PotentialPurchase> PotentialPurchase { get; set; }
    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
 
    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Databases\\Outlay.db;");
        }
    }
}

