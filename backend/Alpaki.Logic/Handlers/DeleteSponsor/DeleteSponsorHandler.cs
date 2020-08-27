using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.DeleteSponsor
{
    public class DeleteSponsorHandler : IRequestHandler<DeleteSponsorRequest, DeleteSponsorResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public DeleteSponsorHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<DeleteSponsorResponse> Handle(DeleteSponsorRequest request, CancellationToken cancellationToken)
        {
            var sponsor = await _databaseContext.Sponsors.Include(s => s.Dreams).SingleAsync(s => s.SponsorId == request.SponsorId, cancellationToken);
            sponsor.Dreams.Clear();

            _databaseContext.Sponsors.Remove(sponsor);

            await _databaseContext.SaveChangesAsync();

            return new DeleteSponsorResponse();
        }
    }
}
