//using Common;
//using Microsoft.Extensions.DependencyInjection;
//using System.Text.RegularExpressions;
//using Telegram.Bot;
//using Telegram.Bot.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;

//namespace TelegramBot
//{
//    public class TelegramBotService
//    {
//        private readonly ITelegramBotClient _botClient;
//        private readonly IServiceProvider _services;

//        public TelegramBotService(string botToken, IServiceProvider services)
//        {
//            _botClient = new TelegramBotClient(botToken);
//            _services = services;
//        }

//        public async Task StartAsync()
//        {
//            using var scope = _services.CreateScope();
//            var expenseService = scope.ServiceProvider.GetRequiredService<IExpenseService>();

//            var cts = new CancellationTokenSource();
//            _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions
//            {
//                AllowedUpdates = Array.Empty<UpdateType>() // Отримувати всі оновлення
//            }, cts.Token);

//            Console.WriteLine("✅ Телеграм-бот запущений!");
//            await Task.Delay(-1); // Запуск у фоновому режимі
//        }

//        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Type != UpdateType.Message || update.Message?.Text == null) return;

//            var message = update.Message;
//            var chatId = message.Chat.Id;
//            var text = message.Text.ToLower();

//            using var scope = _services.CreateScope();
//            var expenseService = scope.ServiceProvider.GetRequiredService<IExpenseService>();

//            if (text.StartsWith("/add"))
//            {
//                var match = Regex.Match(text, @"^/add (\d+) (.+)$");
//                if (match.Success)
//                {
//                    decimal amount = decimal.Parse(match.Groups[1].Value);
//                    string description = match.Groups[2].Value;

//                    expenseService.AddExpense(new Outlay
//                    {
//                        Amount = amount,
//                        Description = description,
//                        DateTime = DateTime.Now,
//                        TypeOfExpense = TypesExpenses
//                    });

//                    await botClient.SendTextMessageAsync(chatId, $"✅ Витрата {amount} грн додана!");
//                }
//                else
//                {
//                    await botClient.SendTextMessageAsync(chatId, "⚠️ Використовуйте формат: `/add 100 Продукти`");
//                }
//            }
//            else if (text.StartsWith("/list"))
//            {
//                var expenses = expenseService.GetAllExpenses();
//                if (expenses.Count == 0)
//                {
//                    await botClient.SendTextMessageAsync(chatId, "📭 Список витрат порожній!");
//                }
//                else
//                {
//                    string response = "📜 Ваші витрати:\n";
//                    foreach (var expense in expenses)
//                    {
//                        response += $"{expense.DateTime:dd.MM} - {expense.Amount} грн - {expense.Description}\n";
//                    }
//                    await botClient.SendTextMessageAsync(chatId, response);
//                }
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(chatId, "🔹 Доступні команди:\n" +
//                                                              "/add 100 Продукти - Додати витрату\n" +
//                                                              "/list - Переглянути витрати");
//            }
//        }

//        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//        {
//            Console.WriteLine($"❌ Помилка в боті: {exception.Message}");
//            return Task.CompletedTask;
//        }
//    }
//}

