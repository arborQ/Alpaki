using System.Threading.Tasks;
using Alpaki.Moto.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Alpaki.Moto.Database
{
    public class MotoDatabaseContext : DbContext, IMotoDatabaseContext
    {
        private readonly ILogger<MotoDatabaseContext> _logger;

        public DbSet<Brand> Brands { get; set; }

        public MotoDatabaseContext(DbContextOptions<MotoDatabaseContext> options, ILogger<MotoDatabaseContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public Task ReloadAsync<T>(T entity) where T : class
        {
            return Entry(entity).ReloadAsync();
        }
    }
}
