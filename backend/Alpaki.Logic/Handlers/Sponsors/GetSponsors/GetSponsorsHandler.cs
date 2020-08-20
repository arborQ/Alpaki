using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Logic.Extensions;
using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;
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
            var query = _dbContext.Sponsors.AsNoTracking();

            if (request.CooperationType.HasValue)
            {
                query = query.Where(x => x.CooperationType == request.CooperationType.Value);
            }
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.Search)
                    || x.Brand.Contains(request.Search)
                    || x.Mail.Contains(request.Search)
                    || x.ContactPerson.Contains(request.Search)
                    || x.PhoneNumber.Contains(request.Search)
                );
            }

            query = query.OrderByProperty(request.OrderBy, request.Asc);

            if (request.Page.HasValue)
            {
                query = query.Paged(request.Page.Value);
            }
            
            var sponsors = await query.ToListAsync(cancellationToken: cancellationToken);
            
            return new GetSponsorsResponse
            {
                Sponsors = sponsors.Select(x=>new SponsorItem
                {
                    SponsorId = x.SponsorId,
                    Name = x.Name,
                    ContactPerson = x.ContactPerson,
                    PhoneNumber = x.PhoneNumber,
                    Mail = x.Mail,
                    Brand = x.Brand,
                    CooperationType = x.CooperationType
                }).ToList()
            };
        }
    }
}