using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;
using Microsoft.AspNetCore.Authorization; // Add if using authorization
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Administrator")] // Uncomment to secure the controller
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                var user = new User
                {
                    Username = registerRequest.Username,
                    Email = registerRequest.Email,
                    Password = registerRequest.Password,
                    Role = registerRequest.Role,
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    Address = registerRequest.Address, // Single string
                    PhoneNumber = registerRequest.PhoneNumber,
                    Gender = registerRequest.Gender
                };

                var createdUser = await _userService.Register(user);
                return Ok(createdUser);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.Login(loginRequest.Email, loginRequest.Password);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // New Endpoint: Get all activated users
        [HttpGet("activated")]
        public async Task<IActionResult> GetActivatedUsers()
        {
            var activatedUsers = await _userService.GetActivatedUsers();
            return Ok(activatedUsers);
        }

        // New Endpoint: Get all deactivated users
        [HttpGet("deactivated")]
        public async Task<IActionResult> GetDeactivatedUsers()
        {
            var deactivatedUsers = await _userService.GetDeactivatedUsers();
            return Ok(deactivatedUsers);
        }

        // New Endpoint: Activate User
        [HttpPost("{id}/activate")]
        // [Authorize(Roles = "Administrator")] // Uncomment to secure the endpoint
        public async Task<IActionResult> ActivateUser(string id)
        {
            try
            {
                await _userService.ActivateUser(id);
                return Ok(new { message = "User activated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        // [Authorize(Roles = "Administrator")] // Uncomment to secure the endpoint
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest updateRequest)
        {
            try
            {
                await _userService.UpdateUser(id, updateRequest);
                return Ok(new { message = "User updated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // New Endpoint: Deactivate User
        [HttpPost("{id}/deactivate")]
        // [Authorize(Roles = "Administrator")] // Uncomment to secure the endpoint
        public async Task<IActionResult> DeactivateUser(string id)
        {
            try
            {
                await _userService.DeactivateUser(id);
                return Ok(new { message = "User deactivated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DTOs
        // DTOs
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterRequest
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public UserRole Role { get; set; }

            // New Fields
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; } // Single string
            public string PhoneNumber { get; set; }
            public Gender Gender { get; set; }
        }

        // New DTO for Updating User (Password excluded)
        public class UpdateUserRequest
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public UserRole Role { get; set; }

            // New Fields
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; } // Single string
            public string PhoneNumber { get; set; }
            public Gender Gender { get; set; }
        }








    }
}
