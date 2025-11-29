using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.ChatEngine.Commands.Dto
{
    public class DishProductUpdateDto
    {
        public int Id { get; set; }

        public int? DishId { get; set; }
        public string? DishName { get; set; }

        public int? ProductId { get; set; }
        public string? ProductName { get; set; }

        public double Quantity { get; set; }
        public string Unit { get; set; } = "г";
    }
}

