using System.Globalization;
using Application;
using Common;
using DataAccess.Entities;
using Telegram.Bot.Types.Enums;
using TelegramBot.ChatEngine.Commands;

namespace CostsBot.ExceptionCommand
{
    [Route("/getDailyExpenses", "Get daily expenses")]
    internal class GetDailyExpensesCommand : ITelegramCommand
    {
        private readonly IExpenseService _expenseService;

        public GetDailyExpensesCommand(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public void DefineStages(StageMapBuilder builder) { }

        public Task<StageResult> Execute(TelegramMessageContext ctx)
        {
            DateTime today = DateTime.Now.Date;
            var purchases = _expenseService.GetAllExpenses()
                .Where(p => p.DateTime.Date == today)
                .ToList();

            return GenerateResponse(purchases, today);
        }

        private Task<StageResult> GenerateResponse(List<Outlay> purchases, DateTime date)
        {
            if (purchases.Count == 0)
            {
                return SendResponse("There are no purchases for today!");
            }

            decimal totalSum = purchases.Sum(p => p.Amount);
            var categorySums = purchases
                .GroupBy(p => p.TypeOfExpense)
                .Select(g => new { Category = GetExpenseType((TypesExpenses)g.Key), Sum = g.Sum(p => p.Amount) })
                .ToList();

            string dateFormatted = date.ToString("dd.MM.yyyy");
            string response = $"Your purchases for {dateFormatted}:\n```\n";
            response += FormatPurchases(purchases);
            response += $"\nTotal spent: {totalSum:F2}\n";
            response += "Spending by categories:\n";

            foreach (var category in categorySums)
            {
                response += $"{category.Category}: {category.Sum:F2}\n";
            }

            return SendResponse(response + "```");
        }

        private string FormatPurchases(List<Outlay> purchases)
        {
            const int idWidth = 5;
            const int descWidth = 15;
            const int dateWidth = 10;
            const int typeWidth = 15;
            const int amountWidth = 10;

            string result =
                $"{"ID",-idWidth} | {"Description",-descWidth} | {"Date",-dateWidth} | {"Type",-typeWidth} | {"Amount",-amountWidth}\n" +
                $"{new string('-', idWidth)}-|-{new string('-', descWidth)}-|-{new string('-', dateWidth)}-|-{new string('-', typeWidth)}-|-{new string('-', amountWidth)}\n";

            foreach (var purchase in purchases)
            {
                string id = purchase.Id.ToString().Length > idWidth
                    ? purchase.Id.ToString().Substring(0, idWidth - 1) + "~"
                    : purchase.Id.ToString();

                string description = purchase.Description.Length > descWidth
                    ? purchase.Description.Substring(0, descWidth - 3) + "..."
                    : purchase.Description;

                string type = GetExpenseType((TypesExpenses)purchase.TypeOfExpense);
                type = type.Length > typeWidth
                    ? type.Substring(0, typeWidth - 3) + "..."
                    : type;

                result += $"{id,-idWidth} | {description,-descWidth} | {purchase.DateTime:dd.MM.yyyy} | {type,-typeWidth} | {purchase.Amount,amountWidth:F2}\n";
            }

            return result;
        }

        private string GetExpenseType(TypesExpenses type) => type.ToString();

        private Task<StageResult> SendResponse(string response)
        {
            return ContentResponse.New(new()
            {
                Text = response,
                ParseMode = ParseMode.MarkdownV2
            });
        }
    }
}


