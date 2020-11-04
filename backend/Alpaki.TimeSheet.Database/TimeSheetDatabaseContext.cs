using Alpaki.TimeSheet.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.TimeSheet.Database
{
    public class TimeSheetDatabaseContext : DbContext, ITimeSheetDatabaseContext
    {
        public DbSet<TimeEntry> TimeEntries { get; set; }

        public DbSet<Project> Projects { get; set; }
        
        public TimeSheetDatabaseContext(DbContextOptions<TimeSheetDatabaseContext> options)
            : base(options)
        {
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeEntry>().HasKey(t => new { t.Year, t.Month, t.UserId, t.Day });

            SeedData(modelBuilder);
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
