using Alpaki.TimeSheet.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.TimeSheet.Database
{
    public interface ITimeSheetDatabaseContext
    {
        void Migrate();

        DbSet<TimeEntry> TimeEntries { get; }

        DbSet<Project> Projects { get; }
    }
}
