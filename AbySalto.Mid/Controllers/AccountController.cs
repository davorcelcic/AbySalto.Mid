using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbySalto.Mid.WebApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] string userName, [FromQuery] string password, [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                return BadRequest("Username, password and email are required");
            var user = await _userService.RegisterAsync(userName, password, email);
            if (user == null)
                return BadRequest("Username already exists");
            return Ok(new { user.Id, user.UserName, user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string userName, [FromQuery] string password)
        {
            var user = await _userService.AuthenticateAsync(userName, password);
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(new { user.Id, user.UserName, user.Email });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(userId))
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok(new { Message = "User is loged out." });
            }
            return BadRequest(new { Message = "No loged user." });
        }

        [Authorize]
        [HttpGet("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity?.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }
            return Ok(new { UserName = username, UserId = userId });
        }
    }
}
