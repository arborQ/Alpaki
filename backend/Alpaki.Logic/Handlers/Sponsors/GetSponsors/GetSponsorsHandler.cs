using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsors
{
    public class GetSponsorsHandler : IRequestHandler<GetSponsorsRequest, GetSponsorsResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public GetSponsorsHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetSponsorsResponse> Handle(GetSponsorsRequest request, CancellationToken cancellationToken)
        {
            var sponsors = await _dbContext.Sponsors.ToListAsync(cancellationToken: cancellationToken);
            return new GetSponsorsResponse
            {
                Sponsors = sponsors.Select(x=>new GetSponsorsResponse.SponsorItem
                {
                    Id = x.SponsorId,
                    Name = x.Name,
                    ContactPerson = x.ContactPerson,
                    PhoneNumber = x.PhoneNumber,
                    Mail = x.Mail
                }).ToList()
            };
        }
    }
}