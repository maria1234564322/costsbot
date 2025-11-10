using DataAccess.Entities;
using DataAccess.IRepositories;


namespace DataAccess.Repositories
{
    public class PeriodicReminderRepository : Repository<PeriodicReminder>, IPeriodicReminderRepository
    {
        private readonly ApplicationDbContext _context;

        public PeriodicReminderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       

        public void AddPeriodicReminders(PeriodicReminder periodicReminder)
        {
            _context.PeriodicReminders.Add(periodicReminder);
            _context.SaveChanges();
        }

        public bool DeletePeriodicReminders(int id)
        {
            var reminder = _context.PeriodicReminders.FirstOrDefault(r => r.Id == id);
            if (reminder == null)
                return false;

            _context.PeriodicReminders.Remove(reminder);
            return true;
        }

        public List<PeriodicReminder> GetAllPeriodicReminders()
        {
            return _context.PeriodicReminders.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    } 
}
