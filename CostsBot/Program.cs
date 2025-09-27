using Application;
using Application.IServiсe;
using Application.Service;
using CostsBot;
using DataAccess;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Loader;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Implementation.Dro;
using TelegramBot.ChatEngine.Setup;
using static TelegramBot.ChatEngine.Commands.Repsonses.Button;
using static TelegramBot.ChatEngine.Commands.Repsonses.Menu;

internal class Program
{
    private static void Main(string[] args)
    {
        string appSettingsFileName = "appsettings.Development.json";

        //if (args.Length != 0)
       // {
       //     appSettingsFileName = $"appSettings.{args[0]}.json";
       // }

        var builder = new MessageHandlerBuilder();
        var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appSettingsFileName);
        var config = new ConfigurationBuilder().AddJsonFile(appSettingsPath).Build();

        var connectionString = config["ConnectionStrings:ApplicationDb"];
        builder.Services.AddCommandsAndStages();

        var client = new TelegramBotClient("7670092141:AAEaqwDf7f6lFtZz5qrB-vnF_VFYyNkpyf0");
        builder.Services.AddSingleton<ITelegramBotClient>(client);
        builder.Services.AddSingleton<ExpenseReminder>(); // RemindeR
        builder.Services.AddTransient<IExpenseReminderRepository, ExpenseReminderRepository>();

        // Реєстрація сервісів
        builder.Services.AddScoped<IExpenseService, ExpenseService>();

        // Реєстрація репозиторіїв
        builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IExpenseReminderRepository, ExpenseReminderRepository>();
        builder.Services.AddScoped<IPotentialPurchaseRepository, PotentialPurchaseRepository>();


        builder.Services.AddDbContext<ApplicationDbContext>(b => b.UseSqlite(connectionString));
        builder.Services.AddTransient<IPotentialPurchaseRepository, PotentialPurchaseRepository>();
        builder.Services.AddTransient<IPotentialPurchaseService, PotentialPurchaseService>();
        builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
        builder.Services.AddTransient<IExpenseService, ExpenseService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();

        var updateHandler = new TelegramUpdateHandler();
        Func<ContentResultV2, Task<SentTelegramMessage>> sendAction = async resp =>
        {
            Message messageResponse = default;
            var contentResult = resp;

            bool hasMenu = contentResult.Menu != null;
            IReplyMarkup menu = null;

            if (hasMenu)
            {
                var type = contentResult.Menu.Type;
                var menuData = contentResult.Menu.MenuScheme;
                switch (type)
                {
                    case MenuType.MessageMenu:
                        menu = new InlineKeyboardMarkup(menuData.Select(x => x.Select(y =>
                        {
                            return y.Type switch
                            {
                                ButtonContentType.Text => InlineKeyboardButton.WithCallbackData(y.Text, y.Text),
                                ButtonContentType.Url => InlineKeyboardButton.WithUrl(y.Text, y.Url),
                                ButtonContentType.CallbackData => InlineKeyboardButton.WithCallbackData(y.Text, y.CallbackData),
                                _ => throw new NotImplementedException()
                            };
                        })));
                        break;
                    case MenuType.MenuKeyboard:
                        menu = new ReplyKeyboardMarkup(menuData.Select(x => x.Select(y => new KeyboardButton(y.Text))))
                        {
                            ResizeKeyboard = true
                        };
                        break;
                }
            }

            messageResponse = await client.SendTextMessageAsync(
                chatId: contentResult.ChatId,
                text: contentResult.Text,
                replyMarkup: menu,
                parseMode: resp.ParseMode,
                disableWebPagePreview: resp.DisableWebPagePreview);

            return new SentTelegramMessage
            {
                SentMessage = messageResponse
            };
        };

        builder.MessageTransportation.RegisterSenderAction(sendAction);

        var handler = builder.Build();

        Console.WriteLine("App started. Applying migrations.");
        using var ctx = handler.ServiceProvider.GetService<ApplicationDbContext>();
        var migrations = ctx.Database.GetPendingMigrations();
        ctx.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
        updateHandler.Handler = handler;
        client.StartReceiving(updateHandler);

        //запуск ExpenseReminder
        var reminder = handler.ServiceProvider.GetRequiredService<ExpenseReminder>();
        reminder.LoadUsersFromPersistentStorage();

        LoopConsoleClosing();
        static void LoopConsoleClosing()
        {
            var ended = new ManualResetEventSlim();
            var starting = new ManualResetEventSlim();

            AssemblyLoadContext.Default.Unloading += ctx =>
            {
                Console.WriteLine("Unloding fired");
                starting.Set();
                Console.WriteLine("Очікування завершення");
                ended.Wait();
            };

            Console.WriteLine("Очікування сигналів");
            starting.Wait();

            Console.WriteLine("Отриманий сигнал вимикається");
            Thread.Sleep(5000);
            ended.Set();
        }
    }
}