using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayMenuController : ControllerBase
    {
        private readonly IDayMenuService _dayMenuService;

        public DayMenuController(IDayMenuService dayMenuService)
        {
            _dayMenuService = dayMenuService;
        }

        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var menus = _dayMenuService.GetAllDayMenus();
            return Ok(menus);
        }

       
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var menu = _dayMenuService.GetDayMenuById(id);
            if (menu == null)
                return NotFound($"DayMenu with ID {id} not found.");

            return Ok(menu);
        }

        
        [HttpPost("create")]
        public IActionResult Create([FromBody] DayMenu menu)
        {
            if (menu == null)
                return BadRequest("DayMenu data is required.");

            _dayMenuService.AddDayMenu(menu);
            return Ok(new { message = "DayMenu created successfully" });
        }

        
        [HttpPut("update")]
        public IActionResult Update([FromBody] DayMenu menu)
        {
            if (menu == null || menu.Id == 0)
                return BadRequest("Invalid DayMenu data.");

            _dayMenuService.UpdateDayMenu(menu);
            return Ok(new { message = "DayMenu updated successfully" });
        }

       
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _dayMenuService.DeleteDayMenu(id);
            return Ok(new { message = "DayMenu deleted successfully" });
        }
    }
}
