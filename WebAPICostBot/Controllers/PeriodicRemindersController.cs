using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.ChatEngine.Commands.Dto;

namespace WebAPICostBot.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class PeriodicRemindersController : ControllerBase
        {
            private readonly IPeriodicReminderService _periodicReminderService;

            public PeriodicRemindersController(IPeriodicReminderService periodicReminderService)
            {
                _periodicReminderService = periodicReminderService;
            }

            [HttpPost("add")]
public IActionResult AddPeriodicReminder([FromBody] PeriodicReminderDto dto)
{
    if (dto == null)
        return BadRequest("Reminder data is required");

    if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
        return BadRequest("Invalid time format. Expected format: HH:mm:ss");

    var reminder = new PeriodicReminder
    {
        Title = dto.Title,
        Time = parsedTime,
        ActiveDays = (Common.Day)dto.ActiveDays,
        IsActive = dto.IsActive,
        CreatedAt = dto.CreatedAt
    };

    _periodicReminderService.AddPeriodicReminders(reminder);
    return Ok(new { message = "Periodic reminder added successfully" });
}


            [HttpGet("all")]
            public IActionResult GetAllPeriodicReminders()
            {
                var reminders = _periodicReminderService.GetAllPeriodicReminders();
                return Ok(reminders);
            }

            [HttpDelete("delete/{id}")]
            public IActionResult DeletePeriodicReminder(int id)
            {
                try
                {
                    bool result = _periodicReminderService.DeletePeriodicReminders(id);
                    if (!result)
                        return NotFound(new { message = $"Reminder with ID {id} not found" });

                    return Ok(new { message = $"Reminder with ID {id} deleted successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            [HttpPost("save")]
            public IActionResult SaveChanges()
            {
                try
                {
                    _periodicReminderService.SaveChanges();
                    return Ok(new { message = "Changes saved successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }
        }
}


