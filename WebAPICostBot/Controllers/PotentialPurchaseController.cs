using Application;
using Application.IServiсe;
using Common;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.ChatEngine.Commands.Dto;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PotentialPurchaseController : ControllerBase
    {
        private readonly IPotentialPurchaseService _potentialPurchaseService;

        public PotentialPurchaseController(IPotentialPurchaseService potentialPurchaseService)
        {
            _potentialPurchaseService = potentialPurchaseService;
        }

        // Вивід усіх потенційних покупок
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var purchases = _potentialPurchaseService.GetAllPotentialPurchases();
            return Ok(purchases);
        }

        // Видалення покупки за Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _potentialPurchaseService.DeletePotentialPurchase(id);

            if (!success)
            {
                return NotFound($"Покупку з Id={id} не знайдено");
            }

            return NoContent(); // 204
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreatePotentialPurchaseDto dto)
        {
            if (dto == null)
                return BadRequest("Некоректні дані");

            var purchase = new PotentialPurchase
            {
                Name = dto.Name,
                Price = dto.Price,
                Priority = (PriorityLevel)dto.Priority,
                Status = (StatusWish)dto.Status
            };

            _potentialPurchaseService.AddPotentialPurchase(purchase);

            return CreatedAtAction(nameof(GetAll), new { id = purchase.Id }, purchase);
        }

    }
}

