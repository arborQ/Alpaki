using System.Text.Json;
using System.Text.Json.Serialization;
using Alpaki.CrossCutting.ValueObjects;
using Alpaki.TimeSheet.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.TimeSheet.Database
{
    public class TimeSheetDatabaseContext : DbContext, ITimeSheetDatabaseContext
    {
        public DbSet<TimeEntry> TimeEntries { get; set; }

        public DbSet<TimeSheetPeriod> TimeSheetPeriods { get; set; }

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
            modelBuilder
                .Entity<TimeSheetPeriod>()
                .HasKey(t => new { t.Year, t.Month, t.UserId });

            modelBuilder
                .Entity<TimeSheetPeriod>()
                .Property(p => p.Year)
                .HasConversion(year => year.Value, year => Year.From(year));
            modelBuilder
                .Entity<TimeSheetPeriod>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder
                .Entity<TimeSheetPeriod>()
                .Property(p => p.Month)
                .HasConversion(month => month.Value, month => new Month(month));

            modelBuilder
                .Entity<TimeSheetPeriod>()
                .Property(p => p.UserId)
                .HasConversion(userId => userId.Value, userId => new UserId(userId));

            modelBuilder
                .Entity<TimeEntry>()
                .Property(p => p.Year)
                .HasConversion(year => year.Value, year => Year.From(year));

            modelBuilder
                .Entity<TimeEntry>()
                .Property(p => p.Month)
                .HasConversion(month => month.Value, month => new Month(month));

            modelBuilder
                .Entity<TimeEntry>()
                .HasOne(r => r.TimeSheet)
                .WithMany(r => r.TimeEntries)
                .HasForeignKey(t => new { t.Year, t.Month, t.UserId });

            modelBuilder
                .Entity<TimeEntry>()
                .Property(p => p.UserId)
                .HasConversion(userId => userId.Value, userId => new UserId(userId));

            var jsonSerializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };
            modelBuilder
                .Entity<DomainEvent<TimeSheetEventData, TimeSheetEventType>>()
                .ToTable("PeriodDomainEvents", "TimeSheet")
                .Property(p => p.EventData)
                .HasConversion(
                    data => JsonSerializer.Serialize(data, jsonSerializerOptions), 
                    st => JsonSerializer.Deserialize<TimeSheetEventData>(st, jsonSerializerOptions)
                );

            SeedData(modelBuilder);
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
