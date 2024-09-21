using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Dependency Injection
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            await _userService.RegisterUserAsync(user);
            return Ok("User registered successfully.");
        }

        // POST api/user/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var user = await _userService.AuthenticateAsync(username, password);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok(user);
        }

        // GET api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
