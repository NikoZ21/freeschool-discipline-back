using DisciplineBackend_WebApi.Entities;
using DisciplineBackend_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;


namespace DisciplineBackend_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
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

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(issuer: configuration.GetValue<string>("AppSettings:Issuer"), audience: configuration.GetValue<string>("AppSettings:Audiance"), claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: creds);

            return new  JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
