using Common;
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
        public TimeSpan Time { get; set; }
        public Day ActiveDays { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } 
    }
}
