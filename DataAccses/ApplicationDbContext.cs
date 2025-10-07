using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Outlay> Outlay { get; set; }
    public DbSet<PotentialPurchase> PotentialPurchase { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
 
    public ApplicationDbContext()
    {
    }

}

