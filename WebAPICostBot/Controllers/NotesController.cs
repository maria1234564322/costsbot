using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICostBot.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotesController : ControllerBase
  {
      private readonly INoteService _noteService;

      public NotesController(INoteService noteService)
      {
        _noteService = noteService;
      }

           
      [HttpGet("all")]
      public IActionResult GetAll()
      {
          var notes = _noteService.GetAllNotes();
          return Ok(notes);
      }

          
      [HttpGet("{id}")]
      public IActionResult GetById(int id)
      {
         var note = _noteService.GetNoteById(id);
         if (note == null)
         return NotFound($"Note with ID {id} not found.");

         return Ok(note);
      }

      [HttpPost("create")]
      public IActionResult Create([FromBody] Note note)
      {
         if (note == null)
         return BadRequest("Note is required");

         _noteService.AddNote(note);
        return Ok(new { message = "Note created successfully" });
      }

            
      [HttpPut("update")]
      public IActionResult Update([FromBody] Note note)
      {
        if (note == null || note.Id == 0)
        return BadRequest("Invalid note data");
        _noteService.UpdateNote(note);
        return Ok(new { message = "Note updated successfully" });
      }

 
      [HttpDelete("delete/{id}")]
      public IActionResult Delete(int id)
      {
         _noteService.DeleteNote(id);
         return Ok(new { message = "Note deleted successfully" });
      }

        
      [HttpGet("reminders")]
      public IActionResult GetReminders()
      {
        var notes = _noteService.GetAllNotes()
        .Where(n => n.IsReminderSet && n.ReminderDate.HasValue)
        .OrderBy(n => n.ReminderDate)
        .ToList();
        return Ok(notes);
      }
  }
}

