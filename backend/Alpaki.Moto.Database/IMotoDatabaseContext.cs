using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Moto.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Alpaki.Moto.Database
{
    public interface IMotoDatabaseContext
    {
        void EnsureCreated();

        void Migrate();

        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task ReloadAsync<T>(T entity) where T : class;

        DbSet<Brand> Brands { get; }

    }
}
