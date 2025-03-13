using CostsBot.Commands;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;


namespace CostsBot.ExceptionCommand;

[Route("/menuException", "Exception")]
internal class ExceptionMenu : ITelegramCommand
{
    private readonly RoutingTable rt;

    public ExceptionMenu(RoutingTable routingTable)
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
            Text = "Exception",
            Menu = new Menu(Menu.MenuType.MenuKeyboard,
                new[]
                {
                    new[]
                    {
                        new Button(rt.AlternativeRoute<AddExceptionCommand>()),
                        new Button(rt.AlternativeRoute<GetDailyExpensesCommand>()),
                    },

                    new[]
                    { 
                        new Button(rt.AlternativeRoute<GetWeekExpensesCommand>()),
                        new Button(rt.AlternativeRoute<GetMonthlyExpensesCommand>())
                    },

                    new[]
                    {
                     new Button(rt.AlternativeRoute<MenuCommand>())
                    }
                }
            )
        });
    }
}



