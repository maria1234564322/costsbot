using Application;
using TelegramBot.ChatEngine.Commands;
using Telegram.Bot.Types.Enums;
using DataAccess.Entities;
using Common;
using System.Text;

namespace CostsBot.Commands;

[Route("/getWeeklyExpenses", "Get weekly expenses")]
internal class GetWeekExpensesCommand : ITelegramCommand
{
    private readonly IExpenseService _expenseService;

    public GetWeekExpensesCommand(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public void DefineStages(StageMapBuilder builder) { }

    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        DateTime endDate = DateTime.Now.Date;
        DateTime startDate = endDate.AddDays(-6);

        var purchases = _expenseService.GetAllExpenses()
            .Where(p => p.DateTime.Date >= startDate && p.DateTime.Date <= endDate)
            .ToList();

        return GenerateResponse(purchases, startDate, endDate);
    }

    private Task<StageResult> GenerateResponse(List<Outlay> purchases, DateTime startDate, DateTime endDate)
    {
        if (purchases.Count == 0)
        {
            return SendResponse("There are no purchases for the selected period!");
        }

        decimal totalSum = purchases.Sum(p => p.Amount);
        var categorySums = purchases
            .GroupBy(p => p.TypeOfExpense)
            .Select(g => new { Category = GetExpenseType((TypesExpenses)g.Key), Sum = g.Sum(p => p.Amount) })
            .ToList();

        string response = $"⏳📆Your purchases from {startDate:dd.MM.yyyy} to {endDate:dd.MM.yyyy}⏳:\n";
        response += FormatPurchasesAsCsv(purchases);
        response += $"\n💸Total spent💸: {totalSum:F2}\n";
        response += "👛Spending by categories👛:\n";

        foreach (var category in categorySums)
        {
            response += $"{category.Category}: {category.Sum:F2}\n";
        }

        return SendResponse(response);
    }

    private string FormatPurchasesAsCsv(List<Outlay> purchases)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ID,Description,Date,Type,Amount");

        foreach (var purchase in purchases)
        {
            string id = purchase.Id.ToString();
            string description = purchase.Description.Replace(",", " "); 
            string date = purchase.DateTime.ToString("dd.MM.yyyy");
            string type = GetExpenseType((TypesExpenses)purchase.TypeOfExpense).Replace(",", " ");
            string amount = purchase.Amount.ToString("F2");

            sb.AppendLine($"{id},{description},{date},{type},{amount}");
        }

      
        return $"<pre>{sb}</pre>";
    }

    private string GetExpenseType(TypesExpenses type) => type.ToString();

    private Task<StageResult> SendResponse(string response)
    {
        return ContentResponse.New(new()
        {
            Text = response,
            ParseMode = ParseMode.Html
        });
    }
}



