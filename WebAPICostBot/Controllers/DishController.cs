using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        // 🔹 Отримати всі страви
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var dishes = _dishService.GetAllDishes();
            return Ok(dishes);
        }

        // 🔹 Отримати страву за ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dish = _dishService.GetDishById(id);
            if (dish == null)
                return NotFound($"Dish with ID {id} not found.");

            return Ok(dish);
        }

        // 🔹 Створити нову страву
        [HttpPost("create")]
        public IActionResult Create([FromBody] Dish dish)
        {
            if (dish == null)
                return BadRequest("Dish data is required.");

            _dishService.AddDish(dish);
            return Ok(new { message = "Dish created successfully" });
        }

        // 🔹 Оновити страву
        [HttpPut("update")]
        public IActionResult Update([FromBody] Dish dish)
        {
            if (dish == null || dish.Id == 0)
                return BadRequest("Invalid dish data.");

            _dishService.UpdateDish(dish);
            return Ok(new { message = "Dish updated successfully" });
        }

        // 🔹 Видалити страву
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _dishService.DeleteDish(id);
            return Ok(new { message = "Dish deleted successfully" });
        }
    }
}

