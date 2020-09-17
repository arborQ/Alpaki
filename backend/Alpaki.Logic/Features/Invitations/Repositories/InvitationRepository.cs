using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Features.Invitations.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly IDatabaseContext _context;

        public InvitationRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Invitation> GetInvitationAsync(string email, CancellationToken cancellationToken) => await _context.Invitations.SingleOrDefaultAsync(
            x => x.Email.ToLower() == email.ToLower() 
                 && x.Status == InvitationStateEnum.Pending, cancellationToken);

        public async Task AddAsync(Invitation invitation, CancellationToken cancellationToken)
        {
            await _context.Invitations.AddAsync(invitation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Invitation invitation, CancellationToken cancellationToken)
        {
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}