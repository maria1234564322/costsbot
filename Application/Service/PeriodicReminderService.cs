using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;

namespace Application.Service
{
    public class PeriodicReminderService: IPeriodicReminderService
    {
        private readonly IPeriodicReminderRepository _periodicReminderRepository;
        public PeriodicReminderService(IPeriodicReminderRepository periodicReminderRepository)
        {
            _periodicReminderRepository = periodicReminderRepository;
        }

        public void AddPeriodicReminders(PeriodicReminder periodicReminder)
        {
            if (periodicReminder == null)
                throw new ArgumentNullException(nameof(periodicReminder));

            _periodicReminderRepository.Add(periodicReminder);
            _periodicReminderRepository.SaveChanges();
        }

        public bool DeletePeriodicReminders(int id)
        {
            var periodicReminder = _periodicReminderRepository.GetById(id);
            if (periodicReminder == null)
                throw new InvalidOperationException($"Reminder with ID {id} not found.");

            _periodicReminderRepository.Remove(periodicReminder);
            _periodicReminderRepository.SaveChanges();

            return true;
        }

        public List<PeriodicReminder> GetAllPeriodicReminders()
        {
            return _periodicReminderRepository.GetAll();
        }

        public void SaveChanges()
        {
            _periodicReminderRepository.SaveChanges();
           
        }
    }
}
