
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Calories { get; set; }
     
        public ICollection<DishProduct> DishProducts { get; set; } = new List<DishProduct>();
    }
}
