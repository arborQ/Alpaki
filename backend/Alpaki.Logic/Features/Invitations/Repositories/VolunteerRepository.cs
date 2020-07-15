using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Features.Invitations.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly IDatabaseContext _context;

        public VolunteerRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExitsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower()), cancellationToken);
        }
    }
}