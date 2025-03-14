using Application.IServiсe;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;

namespace CostsBot.PotentialPurchaseCommand;

[Route("/getAllPotentialPurchase", "Get all potential purchase")]
internal class GetAllPotentialPurchaseCommand : ITelegramCommand
{
    private readonly RoutingTable rt;
    private readonly IPotentialPurchaseService _pp;

    public GetAllPotentialPurchaseCommand(RoutingTable routingTable, IPotentialPurchaseService pp)
    {
        rt = routingTable;
        _pp = pp;
    }

    public void DefineStages(StageMapBuilder builder)
    {
    }

    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        var purchases = _pp.GetAllPotentialPurchases();
        string response = "Potential purchases:";

        if (purchases.Count == 0)
        {
            response = "The list of potential purchases is empty!";
        }
        else
        {
            foreach (var purchase in purchases)
            {
                response += $"\n🔹{purchase.Id} {purchase.Name}";
            }
        }

        return ContentResponse.New(new()
        {
            Text = response,
            Menu = new Menu(Menu.MenuType.MenuKeyboard,
            [
               [
                 new Button(rt.AlternativeRoute<ShoppingListMenu>()),
                 new Button(rt.AlternativeRoute<MenuCommand>())
               ]
            ])
        });
    }
}
