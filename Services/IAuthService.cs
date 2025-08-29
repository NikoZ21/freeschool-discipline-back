using DisciplineBackend_WebApi.Entities;
using DisciplineBackend_WebApi.Models;

namespace DisciplineBackend_WebApi.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
