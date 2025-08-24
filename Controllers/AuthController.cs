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

        public User user = new();

        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PsswordHash = hashedPassword;

            return Ok(user);
        }
    }
}
