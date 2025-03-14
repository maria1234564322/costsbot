using DataAccess.Entities;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{
    public class ExpenseRepository : Repository<Outlay>, IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<Outlay> GetAllExpenses()
        {
            return _context.Outlay.ToList();
        }

        public void AddExpense(Outlay expense)
        {
            _context.Outlay.Add(expense); 
            _context.SaveChanges();
        }
    }
}


