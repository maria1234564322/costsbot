using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;

namespace CostsBot.PotentialPurchaseCommand;

[Route("/menuShoppingList", "Shopping List")]
internal class ShoppingListMenu : ITelegramCommand
{
    private readonly RoutingTable rt;
    public ShoppingListMenu(RoutingTable routingTable)
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
            Text = "Shopping list",
            Menu = new Menu(Menu.MenuType.MenuKeyboard,
            new[]
               {
                 new[]
                 {
                   new Button(rt.AlternativeRoute<AddPotentialPurchaseCommand>()),
                 },

                new[]
                {
                 new Button(rt.AlternativeRoute<DeletePotentialPurchaseCommand>()),
                },

                 new[]
                 {
                   new Button(rt.AlternativeRoute<GetAllPotentialPurchaseCommand>()),
                 },

                 new[]
                 {
                   new Button(rt.AlternativeRoute<MenuCommand>())
                 }
            })
        });
    }
}

