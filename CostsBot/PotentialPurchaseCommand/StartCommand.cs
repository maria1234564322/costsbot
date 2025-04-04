using DataAccess.IRepositories;
using System.Threading.Tasks;
using TelegramBot.ChatEngine.Commands;


namespace CostsBot
{
    [Route("/start", "Start bot")]
    internal class StartCommand : ITelegramCommand
    {
        private readonly ExpenseReminder _reminder;
        private readonly IExpenseReminderRepository _repository;

        public StartCommand(ExpenseReminder reminder, IExpenseReminderRepository repository)
        {
            _reminder = reminder;
            _repository = repository;
        }

        public void DefineStages(StageMapBuilder builder)
        {
            
        }

        public async Task<StageResult> Execute(TelegramMessageContext ctx)
        {
            long userId = ctx.RecipientUserId;
            long chatId = ctx.RecipientChatId;

            await _repository.AddUserAsync(userId, chatId);
            _reminder.AddUser(userId);

            return ContentResponse.Text("👋 Привіт! Ви успішно зареєстровані.");
        }
    }
}

