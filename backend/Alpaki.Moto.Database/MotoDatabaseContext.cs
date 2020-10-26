using System.Threading.Tasks;
using Alpaki.Moto.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Moto.Database
{
    public class MotoDatabaseContext : DbContext, IMotoDatabaseContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<BrandDomainEvent> BrandDomainEvents { get; set; }

        public MotoDatabaseContext(DbContextOptions<MotoDatabaseContext> options)
            : base(options)
        {
            Database.ini
        }

        private void SeedData(ModelBuilder modelBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            //modelBuilder.Entity<Brand>().Property(b => b.BrandId).HasConversion(s => s.Value, x => new BrandId(x));
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
