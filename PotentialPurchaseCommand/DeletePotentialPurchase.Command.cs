using Application.IServiсe;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;

namespace CostsBot.PotentialPurchaseCommand;

[Route("/deletePotentialPurchase", "Delete potential purchase")]
internal class DeletePotentialPurchaseCommand : ITelegramCommand
{
    private readonly RoutingTable rt;
    public DeletePotentialPurchaseCommand(RoutingTable routingTable)
    {
        rt = routingTable;
    }

    public void DefineStages(StageMapBuilder builder)
    {
        builder.Stage<DeletePurchaseStage>();
    }

    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        return ContentResponse.New(new()
        {
            Text = "Keep track of the potential purchase number:",
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

internal class DeletePurchaseStage : ITelegramStage
{
    private readonly IPotentialPurchaseService _pp;
    public DeletePurchaseStage(IPotentialPurchaseService pp)
    {
        _pp = pp;
    }
    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        if (!int.TryParse(ctx.Message.Text, out int id))
        {
            return Task.FromResult(ContentResponse.Text("❌ Please enter a valid numeric purchase ID!"));
        }
        _pp.DeletePotentialPurchase(id);
        return Task.FromResult(ContentResponse.Text($"🗑️ Removed potential purchase from ID: {id}."));
    }
}


