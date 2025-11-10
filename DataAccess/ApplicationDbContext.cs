using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Outlay> Outlay { get; set; }
    public DbSet<PotentialPurchase> PotentialPurchase { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<PeriodicReminder> PeriodicReminders { get; set; }

    public DbSet<Product> Products { get; set; } 
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishProduct> DishProducts { get; set; }
    public DbSet<DayMenu> DayMenus { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
 
    public ApplicationDbContext()
    {
    }

}

