using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.TimeSheet.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Alpaki.TimeSheet.Database
{
    public interface ITimeSheetDatabaseContext
    {
        void Migrate();

        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TimeEntry> TimeEntries { get; }

        DbSet<TimeSheetPeriod> TimeSheetPeriods { get; }

        DbSet<Project> Projects { get; }
    }
}
