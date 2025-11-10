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
                return SendResponse("There are no purchases for today!");

            string report = BuildMinimalReport(purchases, date);
            return SendResponse(report);
        }

        private string BuildMinimalReport(List<Outlay> purchases, DateTime date)
        {
            var sb = new System.Text.StringBuilder();

            // Заголовок
            sb.AppendLine($"📒 *Daily Expenses — {date:dd.MM.yyyy}*");
            sb.AppendLine();

            // Шапка
            sb.AppendLine($"ID   Description              Amount");
            sb.AppendLine();

            // Рядки
            foreach (var p in purchases)
            {
                string desc = p.Description.Length > 20
                    ? p.Description.Substring(0, 17) + "..."
                    : p.Description;

                sb.AppendLine($"{p.Id,-4} {desc,-20} {p.Amount,10:F2}");
            }

            sb.AppendLine();
            sb.AppendLine($"*TOTAL:* {purchases.Sum(x => x.Amount):F2}");

            return sb.ToString();
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




