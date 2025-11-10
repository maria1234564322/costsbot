
using DataAccess.Entities;
using DataAccess.IRepositories;

namespace Application;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public void AddExpense(Outlay expense)
    {
        if (expense == null)
        {
            throw new ArgumentNullException(nameof(expense));
        }

        _expenseRepository.AddExpense(expense);
    }

    public List<Outlay> GetAllExpenses()
    {
        return _expenseRepository.GetAllExpenses();
    }
}

