using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;

namespace Alpaki.Logic.Handlers.AddSponsor
{
    public class AddSponsorHandler : IRequestHandler<AddSponsorRequest, AddSponsorResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public AddSponsorHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AddSponsorResponse> Handle(AddSponsorRequest request, CancellationToken cancellationToken)
        {
            var newSponsor = new Sponsor
            {
                DisplayName = request.DisplayName,
                ContactPersonName = request.ContactPersonName,
                Email = request.Email,
                Brand = request.Brand,
                CooperationType = request.CooperationType
            };

            await _databaseContext.Sponsors.AddAsync(newSponsor, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return new AddSponsorResponse { SponsorId = newSponsor.SponsorId };
        }
    }
}