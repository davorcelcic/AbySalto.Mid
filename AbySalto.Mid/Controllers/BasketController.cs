using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromQuery] int productId, [FromQuery] int quantity = 1)
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User is not authenticated." });
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");
            if (productId <= 0)
                return BadRequest("Invalid product ID.");
            int numberId;
            bool success = int.TryParse(userId, out numberId);
            if (!success)
                return BadRequest("User ID is not valid.");
            await _basketService.AddProductAsync(numberId, productId, quantity);
            return Ok();
        }
    }
}
