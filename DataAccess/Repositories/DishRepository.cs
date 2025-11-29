using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext GetDbContext() => _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Dish dish)
        {
            _context.Dishes.Add(dish);
        }

        public void Update(Dish dish)
        {
            _context.Dishes.Update(dish);
        }

        public void Remove(Dish dish)
        {
            _context.Dishes.Remove(dish);
        }

        public Dish? GetById(int id)
        {
            return _context.Dishes
                .Include(d => d.DishProducts)
               .ThenInclude(dp => dp.Product)
                .FirstOrDefault(d => d.Id == id);
        }

        public List<Dish> GetAll()
        {
            return _context.Dishes
                .Include(d => d.DishProducts)
               .ThenInclude(dp => dp.Product)
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

