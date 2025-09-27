using Application;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var expenses = _expenseService.GetAllExpenses();
            return Ok(expenses);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] Outlay expense)
        {
            if (expense == null)
                return BadRequest("Expense is required");

            _expenseService.AddExpense(expense);
            return Ok(new { message = "Expense created successfully" });
        }

        // Витрати за поточний день
       
        [HttpGet("today")]
        public IActionResult GetToday()
        {
            var today = DateTime.Now.Date;
            var expenses = _expenseService.GetAllExpenses()
                .Where(e => e.DateTime.Date == today)
                .ToList();

            return Ok(expenses);
        }

      
        // Витрати за поточний календарний тиждень (понеділок-неділя)
      
        [HttpGet("thisWeek")]
        public IActionResult GetThisWeek()
        {
            var today = DateTime.Now.Date;
            var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7; // зсув від понеділка
            var weekStart = today.AddDays(-diff);
            var weekEnd = weekStart.AddDays(7);

            var expenses = _expenseService.GetAllExpenses()
                .Where(e => e.DateTime.Date >= weekStart && e.DateTime.Date < weekEnd)
                .ToList();

            return Ok(expenses);
        }

        // Витрати за поточний календарний місяць
      
        [HttpGet("thisMonth")]
        public IActionResult GetThisMonth()
        {
            var today = DateTime.Now.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var expenses = _expenseService.GetAllExpenses()
                .Where(e => e.DateTime.Date >= monthStart && e.DateTime.Date < monthEnd)
                .ToList();

            return Ok(expenses);
        }
    }
}

