//using Application;
//using DataAccess.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Telegram.Bot.Types.Enums;
//using TelegramBot.ChatEngine.Commands;

//namespace CostsBot.ExceptionCommand
//{
//    [Route("/getExpensesWith", "Sum your + other user's expenses for day, week, month")]
//    internal class GetExpensesWithCommand : ITelegramCommand
//    {
//        private readonly IExpenseService _expenseService;
//        public GetExpensesWithCommand(IExpenseService expenseService)
//        {
//            _expenseService = expenseService;
//        }

//        public void DefineStages(StageMapBuilder builder) { }

//        public Task<StageResult> Execute(TelegramMessageContext ctx)
//        {
            
//            long currentUserId = ctx.RecipientUserId;

//            var parts = ctx.Message.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
//            if (parts.Length < 2 || !long.TryParse(parts[1], out var otherUserId))
//            {
//                return Task.FromResult(ContentResponse.Text("❗ Будь ласка, надішліть команду у форматі:\n`/getExpensesWith 123456789`"));

//            }

//            var expenses = _expenseService.GetAllExpenses();

//            DateTime today = DateTime.Now.Date;
//            DateTime weekStart = today.AddDays(-(int)today.DayOfWeek + 1); 
//            DateTime monthStart = new DateTime(today.Year, today.Month, 1);

//            var summary = new
//            {
//                Day = SumForRange(expenses, currentUserId, otherUserId, today, today.AddDays(1)),
//                Week = SumForRange(expenses, currentUserId, otherUserId, weekStart, today.AddDays(1)),
//                Month = SumForRange(expenses, currentUserId, otherUserId, monthStart, today.AddDays(1))
//            };

//            string text = $"📊 *Сумарні витрати двох користувачів*\n" +
//                          $"🧑‍💻 User 1: `{currentUserId}`\n" +
//                          $"👤 User 2: `{otherUserId}`\n\n" +
//                          $"🗓 *Сьогодні:* `{summary.Day:F2}`\n" +
//                          $"📅 *Цього тижня:* `{summary.Week:F2}`\n" +
//                          $"📆 *Цього місяця:* `{summary.Month:F2}`";

//            return ContentResponse.New(new()
//            {
//                Text = text,
//                ParseMode = ParseMode.Markdown
//            });
//        }

//        private decimal SumForRange(List<Outlay> all, long uid1, long uid2, DateTime from, DateTime to)
//        {
//            return all
//                .Where(e => (e.Id == uid1 || e.Id == uid2) && e.DateTime >= from && e.DateTime < to)
//                .Sum(e => e.Amount);
//        }
//    }
//}
    

