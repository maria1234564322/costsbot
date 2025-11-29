using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.ChatEngine.Commands.Dto
{
    public class DishProductDto
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string Unit { get; set; } = "г";
    }
}

