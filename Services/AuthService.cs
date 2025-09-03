using DisciplineBackend_WebApi.Data;
using DisciplineBackend_WebApi.Entities;
using DisciplineBackend_WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DisciplineBackend_WebApi.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PsswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(issuer: configuration.GetValue<string>("AppSettings:Issuer"), audience: configuration.GetValue<string>("AppSettings:Audience"), claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            bool doesUserAlreadyExist = await context.Users.AnyAsync(u => u.Username == request.Username);

            if (doesUserAlreadyExist)
            {
                return null;
            }

            var user = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PsswordHash = hashedPassword;

            context.Users.Add(user);
            await context.SaveChangesAsync();


            return user;
        }
    }
}
