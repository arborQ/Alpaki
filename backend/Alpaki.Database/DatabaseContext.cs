using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Alpaki.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly ILogger<DatabaseContext> _logger;
        private readonly IJwtGenerator _jwtGenerator;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger, IJwtGenerator jwtGenerator)
            : base(options)
        {
            _logger = logger;
            _jwtGenerator = jwtGenerator;
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>().HasData(
               new[] { "Chcę dostać", "Chcę poznać", "Chcę pojechać", "Chcę kimś się stać", "Chcę komuś coś dać" }.Select((name, index) => new DreamCategory
               {
                   DreamCategoryId = index + 1,
                   CategoryName = name,
               }));

            var adminUser = new User { FirstName = "admin", LastName = "admin", Email = "admin@admin.pl", UserId = 1, Role = UserRoleEnum.Admin };
            modelBuilder.Entity<User>().HasData(adminUser);
            _logger.LogInformation($"[ADMIN]: admin user was created with token: [{_jwtGenerator.Generate(adminUser)}]");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>().HasMany(c => c.Dreams).WithOne(d => d.DreamCategory);
            modelBuilder.Entity<Dream>().HasMany(d => d.RequiredSteps).WithOne(s => s.Dream);

            modelBuilder.Entity<AssignedDreams>().HasKey(ad => new { ad.DreamId, ad.VolunteerId });

            modelBuilder.Entity<Invitation>()
                .HasIndex(x => x.Email)
                .IsUnique();

            SeedData(modelBuilder);
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Dream> Dreams { get; set; }

        public DbSet<DreamCategory> DreamCategories { get; set; }

        public DbSet<DreamCategoryDefaultStep> DreamCategoryDefaultSteps { get; set; }

        public DbSet<DreamStep> DreamSteps { get; set; }

        public DbSet<AssignedDreams> AssignedDreams { get; set; }

        public DbSet<Invitation> Invitations { get; set; }
    }
}
