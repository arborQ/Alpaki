using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;
using Alpaki.DomainLogic.DomainObjects;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Alpaki.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly ILogger<DatabaseContext> _logger;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
            : base(options)
        {
            _logger = logger;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>().HasData(
               new[] { "Chcę dostać", "Chcę poznać", "Chcę pojechać", "Chcę kimś się stać", "Chcę komuś coś dać" }.Select((name, index) => new DreamCategory
               {
                   DreamCategoryId = index + 1,
                   CategoryName = name,
               }));

            var adminPassword = "test123!";
            var adminLogin = "admin@admin.pl";

            var adminUser = new User { FirstName = "admin", LastName = "admin", Email = adminLogin, UserId = 1, Role = UserRoleEnum.Admin, PasswordHash = _passwordHasher.HashPassword(adminPassword) };
            modelBuilder.Entity<User>().HasData(adminUser);
            _logger.LogInformation($"[ADMIN]: admin user was created with token: [{_jwtGenerator.Generate(adminUser)}]");
            _logger.LogInformation($"[ADMIN]: admin user was created with login: [{adminLogin}] password: [{adminPassword}]");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamDomainObject>().Property<long>("DreamId").IsRequired();
            modelBuilder.Entity<DreamDomainObject>().HasKey("DreamId");

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

        public Task ReloadAsync<T>(T entity) where T : class
        {
            return Entry(entity).ReloadAsync();
        }

        public DbSet<DreamDomainObject> DreamsObjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Dream> Dreams { get; set; }

        public DbSet<DreamCategory> DreamCategories { get; set; }

        public DbSet<DreamCategoryDefaultStep> DreamCategoryDefaultSteps { get; set; }

        public DbSet<DreamStep> DreamSteps { get; set; }

        public DbSet<AssignedDreams> AssignedDreams { get; set; }

        public DbSet<Invitation> Invitations { get; set; }
    }
}
