using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IPeriodicReminderRepository : IRepository<PeriodicReminder>
    {
        void SaveChanges();
        void AddPeriodicReminders(PeriodicReminder periodicReminder);
        List<PeriodicReminder> GetAllPeriodicReminders();
        bool DeletePeriodicReminders(int id);
    }
}
