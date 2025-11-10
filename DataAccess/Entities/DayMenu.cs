using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class DayMenu
    {
        public int Id { get; set; }
        public Day DayOfTheweek { get; set; }
        public ReceptionTime ReceptionTime { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
