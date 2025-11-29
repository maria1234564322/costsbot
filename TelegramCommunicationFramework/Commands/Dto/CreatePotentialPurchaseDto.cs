using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.ChatEngine.Commands.Dto
{
    public class CreatePotentialPurchaseDto
    {
        public string Name { get; set; };

        public decimal Price { get; set; }

        public int Priority { get; set; } // 0,1,2

        public int Status { get; set; }  

     
    }
}
