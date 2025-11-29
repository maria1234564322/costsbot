
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class DishProduct
    {
        public int Id { get; set; }
        public int DishId { get; set; }

        [JsonIgnore]
        public Dish? Dish { get; set; }
        public int ProductId { get; set; }  
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; } = "г";
    }
}
