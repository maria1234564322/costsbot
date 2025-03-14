using Application;
using Common;
using TelegramBot.ChatEngine.Commands;
using TelegramBot.ChatEngine.Commands.Repsonses;
using TelegramBot.ChatEngine.Commands.Routing;
using static TypeExecute;

namespace CostsBot.ExceptionCommand
{

    [Route("/addException", "New Exception")]
    internal class AddExceptionCommand : ITelegramCommand
    {
        private readonly RoutingTable rt;
        public AddExceptionCommand(RoutingTable routingTable)
        {
            rt = routingTable;
        }

        public void DefineStages(StageMapBuilder builder)
        {
            builder.Stage<DescriptionExecute>();
            builder.Stage<TypeExecute>();
            builder.Stage<AmountExecute>();
            builder.Stage<MenuCommand>();
        }

        public Task<StageResult> Execute(TelegramMessageContext ctx)
        {
            return ContentResponse.New(new()
            {
                Text = "📝 Enter a description of the expense:",
                Menu = new Menu(Menu.MenuType.MenuKeyboard,
                [
                    [new Button(rt.AlternativeRoute<MenuCommand>())]
                ])
            });
        }
    }
}


internal class DescriptionExecute : ITelegramStage
{
    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        ctx.Cache.Set("description", ctx.Message.Text);
        return Task.FromResult(ContentResponse.Text("💰 Enter the purchase amount:"));
    }
}


internal class TypeExecute : ITelegramStage
{
    public Task<StageResult> Execute(TelegramMessageContext ctx)
    {
        if(!decimal.TryParse(ctx.Message.Text, out _))
        {
            ctx.Response.CanIvokeNext = false;
            return Task.FromResult<StageResult>(ContentResponse.Text($"Please enter a number"));
        }
        ctx.Cache.Set("amount", ctx.Message.Text);

        return ContentResponse.New(new()
        {
            Text = "📂 Select the type of expense:",
            Menu = new Menu(Menu.MenuType.MessageMenu,
            [
                [new Button("Food 🍽️", $"{(int)TypesExpenses.Food}"),            new Button("Transportation 🚘", $"{(int)TypesExpenses.Transportation}"),   new Button("Clothing 🧥", $"{(int)TypesExpenses.Clothing}")],
                [new Button("Entertainment 🪁", $"{(int)TypesExpenses.Entertainment}"),   new Button("Utilities 🏠", $"{(int)TypesExpenses.Utilities}"),        new Button("Gifts 🎁", $"{(int)TypesExpenses.Gifts}")],
                [new Button("Education 🧠", $"{(int)TypesExpenses.Education}"),       new Button("Health 🏋️‍♂️", $"{(int)TypesExpenses.Health}"),           new Button("Other🐶", $"{(int)TypesExpenses.Other}")]
            ])
        });
    }
    internal class AmountExecute : ITelegramStage
    {
        private readonly IExpenseService _expenseService;
        public AmountExecute(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public Task<StageResult> Execute(TelegramMessageContext ctx)
        {
            if(!Enum.TryParse<TypesExpenses>(ctx.Message.Text, out var _))
            {
                ctx.Response.CanIvokeNext = false;
                return Task.FromResult<StageResult>(ContentResponse.Text($"Please select an item from the proposed menu"));
            }
            var description = ctx.Cache.Get<string>("description");
            var type = ctx.Message.Text;
            var amount = ctx.Cache.Get<decimal>("amount");

            var expense = new DataAccess.Entities.Outlay
            {
                Description = description,
                TypeOfExpense = Enum.Parse<TypesExpenses>(type),
                Amount = amount,
                DateTime = DateTime.Now
            };

            _expenseService.AddExpense(expense);
            ctx.Response.InvokeNextImmediately = true;
            return Task.FromResult<StageResult>(ContentResponse.Text($"✅ Cost '{expense.Description}' on {expense.Amount} ₴ added!"));
        }
    }
}

