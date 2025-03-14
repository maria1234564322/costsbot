using TelegramBot.ChatEngine.Commands.Repsonses;

namespace TelegramBot.ChatEngine.Commands;

public class ContentResponse
{
    public static Task<StageResult> New(ContentResultV2 result)
    {
        return Task.FromResult(new StageResult()
        {
            Content = result,
        });
    }
    public static Task<StageResult> TaskText(string text)
    {
        return Task.FromResult(new StageResult()
        {
            Content = new()
            {
                Text = text
            },
        });
    }

    public static StageResult Text(string text)
    {
        return new StageResult()
        {
            Content = new()
            {
                Text = text
            },
        };
    }
}
