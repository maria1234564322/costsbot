using DataAccess.Entities;

namespace Application;

public interface IExpenseService
{
    void AddExpense(Outlay expense);
    List<Outlay> GetAllExpenses();
}
