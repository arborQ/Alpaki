using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsor
{
    public class GetSponsorHandler : IRequestHandler<GetSponsorRequest, SponsorItem>
    {
        private readonly IDatabaseContext _dbContext;

        public GetSponsorHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SponsorItem> Handle(GetSponsorRequest request, CancellationToken cancellationToken)
        {
            var sponsor = await _dbContext.Sponsors.AsNoTracking()
                .FirstOrDefaultAsync(x => x.SponsorId == request.SponsorId, cancellationToken: cancellationToken);

            if (sponsor is null)
                return null;
            
            return new SponsorItem
            {
                SponsorId = sponsor.SponsorId,
                Name = sponsor.Name,
                ContactPerson = sponsor.ContactPerson,
                PhoneNumber = sponsor.PhoneNumber,
                Mail = sponsor.Mail,
                Brand = sponsor.Brand,
                CooperationType = sponsor.CooperationType
            };
        }
    }
}