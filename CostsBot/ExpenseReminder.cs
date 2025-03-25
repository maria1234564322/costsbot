
using Telegram.Bot;


namespace CostsBot
{
    public class ExpenseReminder
    {
        private readonly ITelegramBotClient _bot;
        private readonly HashSet<long> _activeUsers = new();
        private readonly Timer _timer;

        public ExpenseReminder(ITelegramBotClient bot)
        {
            _bot = bot;
            _timer = new Timer(async _ => await SendReminders(), null, TimeSpan.Zero, TimeSpan.FromHours(2));
        }

        public void AddUser(long userId)
        {
            _activeUsers.Add(userId);
        }

        private async Task SendReminders()
        {
            foreach (var userId in _activeUsers)
            {
                await _bot.SendTextMessageAsync(userId, "💰 Чи бажаєте внести нову витрату?");
            }
        }
    }
}
