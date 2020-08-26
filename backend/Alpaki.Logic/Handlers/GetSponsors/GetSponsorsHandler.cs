using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetSponsors
{
    public class GetSponsorsHandler : IRequestHandler<GetSponsorsRequest, GetSponsorsResponse>
    {
        private readonly IUserScopedDatabaseReadContext _userScopedDatabaseReadContext;

        public GetSponsorsHandler(IUserScopedDatabaseReadContext userScopedDatabaseReadContext)
        {
            _userScopedDatabaseReadContext = userScopedDatabaseReadContext;
        }

        public async Task<GetSponsorsResponse> Handle(GetSponsorsRequest request, CancellationToken cancellationToken)
        {
            var sponsors = _userScopedDatabaseReadContext.Sponsors;

            if (request.DreamId.HasValue)
            {
                sponsors = sponsors.Where(s => s.Dreams.Any(d => d.DreamId == request.DreamId.Value));
            }

            if (request.CooperationType.HasValue)
            {
                sponsors = sponsors.Where(s => s.CooperationType == request.CooperationType.Value);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                sponsors = sponsors.Where(s => (s.DisplayName + s.ContactPersonName + s.Email).Contains(request.Search));
            }

            var sponsorsList = await sponsors.Select(GetSponsorsResponse.MapSponsorToSponsorItem).ToListAsync();

            return new GetSponsorsResponse { Sponsors = sponsorsList };
        }
    }
}
