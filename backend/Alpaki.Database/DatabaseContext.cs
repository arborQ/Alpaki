using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Database
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; }

        DbSet<Dream> Dreams { get; }

        void EnsureCreated();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>().HasData(
               new[] { "Chcę dostać", "Chcę poznać", "Chcę pojechać", "Chcę kimś się stać", "Chcę komuś coś dać" }.Select((name, index) => new DreamCategory
               {
                   DreamCategoryId = index + 1,
                   CategoryName = name,
               }));

            modelBuilder.Entity<User>().HasData(new User { FirstName = "admin", LastName = "admin", Email = "admin@admin.pl", UserId = 1, Role = UserRoleEnum.Admin });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>().HasMany(c => c.Dreams).WithOne(d => d.DreamCategory);
            modelBuilder.Entity<Dream>().HasMany(d => d.RequiredSteps).WithOne(s => s.Dream);

            modelBuilder.Entity<AssignedDreams>().HasKey(ad => new { ad.DreamId, ad.VolunteerId });

            SeedData(modelBuilder);
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Dream> Dreams { get; set; }

        public DbSet<DreamCategory> DreamCategories { get; set; }

        public DbSet<DreamStep> DreamSteps { get; set; }

        public DbSet<AssignedDreams> AssignedDreams { get; set; }
    }
}
