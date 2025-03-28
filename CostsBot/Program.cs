﻿using Application;
using Application.IServiсe;
using Application.Service;
using CostsBot;
using DataAccess;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Runtime.Loader;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Implementation.Dro;
using TelegramBot.ChatEngine.Setup;
using static TelegramBot.ChatEngine.Commands.Repsonses.Button;
using static TelegramBot.ChatEngine.Commands.Repsonses.Menu;

string dbPath = "/app/db/Costs.db";
System.Data.SQLite.SQLiteConnection.CreateFile(dbPath);
var builder = new MessageHandlerBuilder();


//var connectionString = builder.Configuration["ConnectionStrings:ApplicationDb"];
builder.Services.AddCommandsAndStages();
var client = new TelegramBotClient("7670092141:AAEaqwDf7f6lFtZz5qrB-vnF_VFYyNkpyf0");
builder.Services.AddSingleton<ITelegramBotClient>(client);
builder.Services.AddDbContext<ApplicationDbContext>(b => b.UseSqlite($"Data Source={dbPath};"));
builder.Services.AddTransient<IPotentialPurchaseRepository, PotentialPurchaseRepository>();
builder.Services.AddTransient<IPotentialPurchaseService, PotentialPurchaseService>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();

var updateHandler = new TelegramUpdateHandler();
Func<ContentResultV2, Task<SentTelegramMessage>>  sendAction = async resp =>
{
    Message messageResponse = default;
    var contentResult = resp;

    bool hasMenu = contentResult.Menu != null;
    IReplyMarkup menu = null;
    //menu preparation
    if (hasMenu)
    {
        var type = contentResult.Menu.Type;
        var menuData = contentResult.Menu.MenuScheme;
        switch (type)
        {
            case MenuType.MessageMenu:
                menu = new InlineKeyboardMarkup(menuData.Select(x => x.Select(y =>
                {
                    switch (y.Type)
                    {
                        case ButtonContentType.Text:
                            return InlineKeyboardButton.WithCallbackData(y.Text, y.Text);
                        case ButtonContentType.Url:
                            return InlineKeyboardButton.WithUrl(y.Text, y.Url);
                        case ButtonContentType.CallbackData:
                            return InlineKeyboardButton.WithCallbackData(y.Text, y.CallbackData);
                        default:
                            throw new NotImplementedException();
                    }
                })));
                break;
            case MenuType.MenuKeyboard:
                menu = new ReplyKeyboardMarkup(menuData.Select(x => x.Select(y => new KeyboardButton(y.Text))))
                {
                    ResizeKeyboard = true
                };
                break;
            default:
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
handler.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
Console.WriteLine("Migrations applied successfully.");
updateHandler.Handler = handler;
client.StartReceiving(updateHandler);
LoopConsoleClosing();
 static void LoopConsoleClosing()
{
    var ended = new ManualResetEventSlim();
    var starting = new ManualResetEventSlim();

    AssemblyLoadContext.Default.Unloading += ctx =>
    {
        System.Console.WriteLine("Unloding fired");
        starting.Set();
        System.Console.WriteLine("Waiting for completion");
        ended.Wait();
    };

    System.Console.WriteLine("Waiting for signals");
    starting.Wait();

    System.Console.WriteLine("Received signal gracefully shutting down");
    Thread.Sleep(5000);
    ended.Set();
}
