using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.RemoveSponsor
{
    public class RemoveSponsorHandler : IRequestHandler<RemoveSponsorRequest,RemoveSponsorResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public RemoveSponsorHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RemoveSponsorResponse> Handle(RemoveSponsorRequest request, CancellationToken cancellationToken)
        {
            var sponsor = await _dbContext.Sponsors.FindAsync(request.Id);

            _dbContext.Sponsors.Remove(sponsor);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new RemoveSponsorResponse();
        }
    }
}