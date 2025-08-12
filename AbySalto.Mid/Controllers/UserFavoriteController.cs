using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbySalto.Mid.WebApi.Controllers
{
    public class UserFavoriteController : ControllerBase
    {
        private readonly UserFavoriteService _favoriteService;

        public UserFavoriteController(UserFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFavorite([FromQuery] int productId)
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User is not authenticated." });
            if (productId <= 0)
                return BadRequest("Invalid product ID.");
            int numberId;
            bool success = int.TryParse(userId, out numberId);
            if (!success)
                return BadRequest("User ID is not valid.");
            var result = await _favoriteService.AddFavoriteAsync(numberId, productId);
            if (!result)
                return BadRequest("Already in favorites");
            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFavorite([FromQuery] int productId)
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User is not authenticated." });
            if (productId <= 0)
                return BadRequest("Invalid product ID.");
            int numberId;
            bool success = int.TryParse(userId, out numberId);
            if (!success)
                return BadRequest("User ID is not valid.");
            var result = await _favoriteService.RemoveFavoriteAsync(numberId, productId);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
