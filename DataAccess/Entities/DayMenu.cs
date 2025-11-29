using Common;


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
