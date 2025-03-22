using CostsBot.ExceptionCommand;
using CostsBot.PotentialPurchaseCommand;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;

namespace CostsBot;
[Route("/menu", "⬅️ Return to the main")]
internal class MenuCommand : ITelegramCommand
{
    private readonly RoutingTable rt;
    public MenuCommand(RoutingTable routingTable)
    {
        rt = routingTable;
    }

    public void DefineStages(StageMapBuilder builder)
    {
    }
    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        return ContentResponse.New(new()
        {
            Text = "Functions:",
            Menu = new Menu(Menu.MenuType.MenuKeyboard,
            new[]
            {
                new[]
                {
                   new Button(rt.AlternativeRoute<ShoppingListMenu>()),
                  new Button(rt.AlternativeRoute<ExceptionMenu>()),
                }
            })
        });
    }
}

