using Application;
using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add([FromBody] PotentialPurchase purchase)
        {
            if (purchase == null)
                return BadRequest("Некоректні дані");

            _potentialPurchaseService.AddPotentialPurchase(purchase);
            return CreatedAtAction(nameof(GetAll), new { id = purchase.Id }, purchase);
        }

    }
}

