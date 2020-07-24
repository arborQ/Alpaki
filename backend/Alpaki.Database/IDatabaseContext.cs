using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Alpaki.Database
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; }

        DbSet<Dream> Dreams { get; }

        DbSet<DreamCategory> DreamCategories { get; }

        DbSet<Invitation> Invitations { get; }

        DbSet<AssignedDreams> AssignedDreams { get; }

        void EnsureCreated();

        void Migrate();

        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
