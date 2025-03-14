using Common;

namespace DataAccess.Entities
{
    public class Outlay
    {
        public long Id { get; set; }  
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public TypesExpenses TypeOfExpense { get; set; }
        public decimal Amount { get; set; }
    }
}
