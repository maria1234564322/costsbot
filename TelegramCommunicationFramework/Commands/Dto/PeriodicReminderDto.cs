using Elmah.ContentSyndication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelegramBot.ChatEngine.Commands.Dto
{
    public class PeriodicReminderDto
    {
        public string Title { get; set; }
        public string Time { get; set; }  
        public Day ActiveDays { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
