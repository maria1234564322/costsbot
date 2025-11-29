using Application.IServiсe;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICostBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

       
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

     
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetAll().FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

    
        [HttpPost("create")]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product data is required.");

            _productService.Add(product);
            return Ok(new { message = "Product created successfully" });
        }

        
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok(new { message = "Product deleted successfully" });
        }
    }
}

