using DataAccess.Entities;

namespace DataAccess.IRepositories;

public interface IExpenseRepository : IRepository<Outlay>
{
    void SaveChanges();
    List<Outlay> GetAllExpenses();
    void AddExpense(Outlay expense);
}
