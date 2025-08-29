using DisciplineBackend_WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisciplineBackend_WebApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
