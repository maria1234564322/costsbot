using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories
{
    public class DayMenuRepository : IDayMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public DayMenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(DayMenu dayMenu)
        {
            _context.DayMenus.Add(dayMenu);
        }

        public void Update(DayMenu dayMenu)
        {
            _context.DayMenus.Update(dayMenu);
        }

        public void Remove(DayMenu dayMenu)
        {
            _context.DayMenus.Remove(dayMenu);
        }

        public DayMenu? GetById(int id)
        {
            return _context.DayMenus
                .Include(dm => dm.Dishes) 
                .FirstOrDefault(dm => dm.Id == id);
        }

        public List<DayMenu> GetAll()
        {
            return _context.DayMenus
                .Include(dm => dm.Dishes)
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
