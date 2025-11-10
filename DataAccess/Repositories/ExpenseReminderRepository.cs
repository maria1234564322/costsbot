using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories
{
    public class ExpenseReminderRepository : IRepository<User>, IExpenseReminderRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseReminderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
         
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(long userId, long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                user = new User
                {
                    UserId = userId,
                    ChatId = chatId
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
         
        }

        public Task RemoveUserAsync(long userId)
        {
          
            return Task.CompletedTask;
        }

        public List<User> GetAll()
        {
      
            var users = _context.Users.ToList();
            return users;
        }

        public User GetById(int id)
        {
            // Шукаємо користувача за первинним ключем (Id)
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                throw new InvalidOperationException($"Користувача з Id = {id} не знайдено.");
            }

            return user;
        }

        public void Add(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Users.Add(entity);
        }

        public void Remove(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Users.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
