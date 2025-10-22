using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Migrations;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PeriodicReminderServise: IPeriodicReminderServise
    {
        private readonly IPeriodicReminderRepository _periodicReminderRepository;
        public PeriodicReminderServise(IPeriodicReminderRepository periodicReminderRepository)
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
