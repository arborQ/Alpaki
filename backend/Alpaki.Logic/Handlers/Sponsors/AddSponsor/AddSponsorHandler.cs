using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.AddSponsor
{
    public class AddSponsorHandler : IRequestHandler<AddSponsorRequest,AddSponsorResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public AddSponsorHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddSponsorResponse> Handle(AddSponsorRequest request, CancellationToken cancellationToken)
        {
            var sponsor = new Sponsor
            {
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                PhoneNumber = request.PhoneNumber,
                Mail = request.Mail,
                Brand = request.Brand,
                CooperationType =  request.CooperationType
            };

            await _dbContext.Sponsors.AddAsync(sponsor, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new AddSponsorResponse(sponsor.SponsorId);
        }
    }
}