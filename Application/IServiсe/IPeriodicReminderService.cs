using DataAccess.Entities;


namespace Application.IServiсe
{
    public interface IPeriodicReminderService
    {
        void SaveChanges();
        void AddPeriodicReminders (PeriodicReminder periodicReminder);
        List<PeriodicReminder> GetAllPeriodicReminders();
        bool DeletePeriodicReminders(int id);
    }
}
