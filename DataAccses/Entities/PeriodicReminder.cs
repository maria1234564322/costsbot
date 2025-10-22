using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class PeriodicReminder
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Час, коли має спрацьовувати нагадування
        public TimeSpan Time { get; set; }
        public List<DayOfWeek> ActiveDays { get; set; } = new List<DayOfWeek>();
        public bool IsActive { get; set; } = true;
        // Дата створення
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
