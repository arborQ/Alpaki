using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models.Invitations;

namespace Alpaki.Logic.Features.Invitations.Repositories
{
    public interface IInvitationRepository
    {
        Task<Invitation> GetInvitationAsync(string email, CancellationToken cancellationToken);
        Task AddAsync(Invitation invitation, CancellationToken cancellationToken);
        Task UpdateAsync(Invitation invitation, CancellationToken cancellationToken);
    }
}