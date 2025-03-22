using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Implementation;

namespace CostsBot;

internal class TelegramUpdateHandler : IUpdateHandler
{
    public TelegramBotMessageHandler Handler { get; set; }
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (Handler == null)
            throw new InvalidOperationException("Handler is null");


        TelegramMessage message = null;
        switch (update.Type)
        {
            case UpdateType.Unknown:
                break;

            case UpdateType.Message:
                {
                    message = new()
                    {
                        ChatId = update.Message.Chat.Id,
                        Text = update.Message.Text,
                        UserId = update.Message.From.Id
                    };
                }
                break;

            case UpdateType.InlineQuery:
                {
                    message = new()
                    {
                        Text = update.CallbackQuery.Data,
                        ChatId = update.CallbackQuery.Message.Chat.Id,
                        UserId = update.Message.From.Id
                    };
                    break;
                }
            case UpdateType.ChosenInlineResult:
                break;

            case UpdateType.CallbackQuery:
                {
                    message = new()
                    {
                        Text = update.CallbackQuery.Data,
                        ChatId = update.CallbackQuery.Message.Chat.Id,
                        UserId = update.CallbackQuery.From.Id
                    };
                }
                break;

            default:
                break;
        }
        if (message == null)
        {
            return;
        }
        await Handler.HandleMessage(message);
        // return Task.CompletedTask;
    }
}
