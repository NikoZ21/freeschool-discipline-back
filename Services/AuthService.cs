using DisciplineBackend_WebApi.Data;
using DisciplineBackend_WebApi.Entities;
using DisciplineBackend_WebApi.Models;

namespace DisciplineBackend_WebApi.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public Task<string?> LoginAsync(UserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<User?> RegisterAsync(UserDto request)
        {
            throw new NotImplementedException();
        }
    }
}
