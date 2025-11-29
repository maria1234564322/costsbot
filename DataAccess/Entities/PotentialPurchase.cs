using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class PotentialPurchase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PriorityLevel Priority { get; set; }
        public StatusWish Status { get; set; }

    }
}
