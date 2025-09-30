using Application.IServiсe;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;

namespace CostsBot.PotentialPurchaseCommand;

[Route("/addPotentialPurchase", "Add potential purchase")]
internal class AddPotentialPurchaseCommand : ITelegramCommand
{
    private readonly RoutingTable rt;
    public AddPotentialPurchaseCommand(RoutingTable routingTable)
    {
        rt = routingTable;
    }

    public void DefineStages(StageMapBuilder builder)
    {
        builder.Stage<SavePurchaseStage>();
    }

    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        return ContentResponse.New(new()
        {
            Text = "Enter the name of the purchase:",
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

internal class SavePurchaseStage : ITelegramStage
{
    private readonly IPotentialPurchaseService _pp;
    public SavePurchaseStage(IPotentialPurchaseService pp)
    {
        _pp = pp;
    }
    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        var Name = ctx.Message.Text;
        _pp.AddPotentialPurchase();
        return Task.FromResult(ContentResponse.Text($"Added potential purchase '{Name}'."));
    }
}

