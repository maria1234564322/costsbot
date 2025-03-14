   
Console.WriteLine();//using Application;
//using Common;
//using DataAccess;
//using DataAccess.Entities;
//using DataAccess.IRepositories;
//using DataAccess.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System.Text.RegularExpressions;

//var services = new ServiceCollection();
//services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite("Data Source=C:\\Databases\\Outlay.db;"));
//services.AddScoped<IExpenseService, ExpenseService>();
//services.AddScoped<IRepository<Outlay>, Repository<Outlay>>();
//services.AddScoped<IExpenseRepository, ExpenseRepository>();

//var provider = services.BuildServiceProvider();
//var expenseService = provider.GetService<IExpenseService>();

//// ✅ Запускаємо Telegram-бота в окремому потоці
//Task.Run(async () =>
//{
//    string botToken = "ВАШ_ТОКЕН_БОТА";
//    var botService = new TelegramBotService(botToken, provider);
//    await botService.StartAsync();
//});

//Console.WriteLine("Телеграм-бот запущений!");

//// 🔹 Ваше консольне меню залишається без змін

//Console.WriteLine("Меню");
//Console.WriteLine("1. Додати витрату");
//Console.WriteLine("2. Видалити витрату");
//Console.WriteLine("3. Додати потенційну покупку");
//Console.WriteLine("4. Видалити потенційну покупку");
//Console.WriteLine("5. Переглянути список витрат за сьогодні");
//Console.WriteLine("6. Переглянути категорії витрат за тиждень");
//Console.WriteLine("7. Переглянути категорії витрат за місяць");

//Console.WriteLine("Виберіть код меню:");

//int choice;
//if (int.TryParse(Console.ReadLine(), out choice))
//{
//    switch (choice)
//    {
//        case 1:
//            Console.Write("Введіть суму витрати: ");
//            string input = Console.ReadLine();
//            Match match = Regex.Match(input, "(\\d+)");
//            string amount = match.Success ? match.Groups[1].Value : "0";

//            Console.WriteLine("Оберіть тип витрати:\n0 - Їжа\n1 - Транспорт\n2 - Одяг\n3 - Розваги\n4 - Комунальні послуги\n5 - Подарунки\n6 - Освіта\n7 - Здоров'я\n8 - Благодійність\n9 - Інше");
//            string category = Console.ReadLine().ToUpper();

//            Console.Write("Додайте нотатку: ");
//            string note = Console.ReadLine();

//            expenseService.AddExpense(new DataAccess.Entities.Outlay
//            {
//                Description = note,
//                DateTime = DateTime.Now,
//                TypeOfExpense = Enum.Parse<TypesExpenses>(category),
//            });

//            Console.WriteLine($"Витрата додана! Сума: {amount} грн, Категорія: {category}, Нотатка: {note}");
//            break;
//        case 2:
//            Console.WriteLine("Введіть ID витрати для видалення: ");
//            string expenseId = Console.ReadLine();
//            Console.WriteLine($"Витрата {expenseId} видалена!");
//            break;
//        case 3:
//            Console.WriteLine("Введіть назву потенційної покупки: ");
//            string purchase = Console.ReadLine();
//            Console.WriteLine("Введіть ціна потенційної покупки: ");
//            string purchasePrise = Console.ReadLine();

//            Console.WriteLine($"Покупка {purchase} додана в список потенційних!");
//            break;
//        case 4:
//            Console.WriteLine("Введіть ID покупки для видалення: ");
//            string purchaseId = Console.ReadLine();
//            Console.WriteLine($"Покупка {purchaseId} видалена!");
//            break;
//        case 5:
//            Console.WriteLine("Список витрат за сьогодні:");
//            break;
//        case 6:
//            Console.WriteLine("Категорії витрат за тиждень:");
//            break;
//        case 7:
//            Console.WriteLine("Категорії витрат за місяць:");
//            break;
//        case 8:
//            Console.WriteLine("Діагама витрат за місяць:");
//            break;
//        default:
//            Console.WriteLine("Помилка!");
//            break;
//    }
//}
//else
//{
//    Console.WriteLine("Будь ласка, введіть числове значення!");
//}

