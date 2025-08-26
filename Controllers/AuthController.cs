using DisciplineBackend_WebApi.Entities;
using DisciplineBackend_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Net.NetworkInformation;


namespace DisciplineBackend_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly User user = new() { Username = "peter", PsswordHash = "AQAAAAIAAYagAAAAEJMFMr3DdNRNE/BQT30a5nWXyRXz5024sonbWD7tYMi+da+9Xw6HRybe49UoVEidnA==" };

        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PsswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User not found.");
            }

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PsswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password.");
            }

            string token = "success"; // Placeholder for token generation logic

            return Ok(token);
        }
    }
}
