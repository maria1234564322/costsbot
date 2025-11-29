using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.ChatEngine.Commands.Dto;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishProductController : ControllerBase
    {
        private readonly IDishProductService _dishProductService;

        public DishProductController(IDishProductService dishProductService)
        {
            _dishProductService = dishProductService;
        }

      
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var dishProducts = _dishProductService.GetAll();
            return Ok(dishProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var dishProduct = _dishProductService.GetById(id);
                return Ok(dishProduct);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] DishProductCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Request body is empty.");

            try
            {
                var created = _dishProductService.AddFromDto(dto);

                return Ok(new
                {
                    message = "DishProduct created successfully",
                    id = created.Id,
                    dishId = created.DishId,
                    productId = created.ProductId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpPut("update")]
        public IActionResult Update([FromBody] DishProductUpdateDto dto)
        {
            if (dto == null || dto.Id == 0)
                return BadRequest("Invalid DishProduct data.");

            try
            {
                var updated = _dishProductService.UpdateFromDto(dto);

                return Ok(new
                {
                    message = "DishProduct updated",
                    id = updated.Id,
                    dishId = updated.DishId,
                    productId = updated.ProductId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dishProduct = _dishProductService.GetById(id);
                if (dishProduct == null)
                    return NotFound(new { message = $"DishProduct with ID {id} not found." });

                _dishProductService.Remove(dishProduct);
                return Ok(new { message = "DishProduct deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

