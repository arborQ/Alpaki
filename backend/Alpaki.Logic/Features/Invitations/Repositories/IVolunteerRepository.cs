using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Features.Invitations.Repositories
{
    public interface IVolunteerRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<bool> ExitsAsync(string email, CancellationToken cancellationToken);
    }
}