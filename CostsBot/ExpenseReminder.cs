using System.Collections.Concurrent;
using Telegram.Bot;
using DataAccess.IRepositories;

namespace CostsBot
{
    public class ExpenseReminder
    {
        private readonly ITelegramBotClient _bot;
        private readonly IExpenseReminderRepository _repository;
        private readonly ConcurrentDictionary<long, byte> _activeUsers = new();
        private readonly CancellationTokenSource _cts = new();

        public ExpenseReminder(ITelegramBotClient bot, IExpenseReminderRepository repository)
        {
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            Task.Run(async () => await LoadUsersFromPersistentStorage()).Wait();

            StartReminderLoop();
        }

        private void StartReminderLoop()
        {
            Console.WriteLine("🚀 Reminder loop started!");

            var reminderTimes = new[] { "11:00", "16:30", "22:00" }
                .Select(t => TimeSpan.Parse(t))
                .ToList();

            var sentToday = new HashSet<TimeSpan>();

            Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    var now = DateTime.Now;
                    var currentTime = now.TimeOfDay;

                    if (now.Hour == 0 && now.Minute == 0)
                    {
                        sentToday.Clear();
                    }

                    foreach (var time in reminderTimes)
                    {
                        if (currentTime.Hours == time.Hours && currentTime.Minutes == time.Minutes && !sentToday.Contains(time))
                        {
                            await SendReminders();
                            sentToday.Add(time);
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), _cts.Token); 
                }
            });
        }


        private async Task SendReminders()
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Відправляємо нагадування...");
            Console.WriteLine($"🧍 Активних користувачів: {_activeUsers.Count}");

            foreach (var userId in _activeUsers.Keys)
            {
                try
                {
                    await _bot.SendTextMessageAsync(userId, "💰 Чи бажаєте внести нову витрату?");
                    Console.WriteLine($"✅ Повідомлення надіслано користувачу {userId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Не вдалося надіслати повідомлення користувачу {userId}: {ex.Message}");
                }
            }
        }

        public void Stop() => _cts.Cancel();

        public void AddUser(long userId)
        {
            if (_activeUsers.TryAdd(userId, 0))
            {
                Console.WriteLine($"🟢 Користувач {userId} доданий до списку нагадувань");
            }
        }

        public void RemoveUser(long userId)
        {
            if (_activeUsers.TryRemove(userId, out _))
            {
                Console.WriteLine($"🔴 Користувач {userId} видалений із списку нагадувань");
            }
        }

        internal async Task LoadUsersFromPersistentStorage()
        {
            Console.WriteLine("📥 Завантаження користувачів із БД...");
            var users = await _repository.GetActiveUsersAsync();

            foreach (var user in users)
            {
                if (_activeUsers.TryAdd(user.ChatId, 0))
                {
                    Console.WriteLine($"🔄 Завантажено користувача UserId: {user.UserId}, ChatId: {user.ChatId}");
                }
            }

            Console.WriteLine($"✅ Загалом завантажено: {_activeUsers.Count} користувачів.");
        }
    }
}
