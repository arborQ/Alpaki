using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateDream
{
    public class UpdateDreamHandler : IRequestHandler<UpdateDreamRequest, UpdateDreamResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public UpdateDreamHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UpdateDreamResponse> Handle(UpdateDreamRequest request, CancellationToken cancellationToken)
        {
            var dream = await _dbContext.Dreams.SingleAsync(x => x.DreamId == request.DreamId, cancellationToken);
            
            if (!string.IsNullOrEmpty(dream.Title))
            {
                dream.Title = request.Title;
            }
            
            if (!string.IsNullOrEmpty(dream.DisplayName))
            {
                dream.DisplayName = request.DisplayName;
            }

            if (request.Age.HasValue)
            {
                dream.Age = request.Age.Value;
            }

            if (!string.IsNullOrEmpty(request.DreamUrl))
            {
                dream.DreamUrl = request.DreamUrl;
            }

            if (!string.IsNullOrEmpty(request.CityName))
            {
                dream.CityName = request.CityName;
            }

            if (!string.IsNullOrEmpty(request.Tags))
            {
                dream.Tags = request.Tags;
            }

            if (request.CategoryId.HasValue)
            {
                dream.DreamCategoryId = request.CategoryId.Value;
            }

            if (request.DreamImageId.HasValue)
            {
                dream.DreamImageId = request.DreamImageId.Value;
            }

            if (request.VolunteerIds != null)
            {
                dream.Volunteers = request.VolunteerIds.Select(v => new Database.Models.AssignedDreams { VolunteerId = v }).ToList();
            }

            if (request.SponsorIds != null)
            {
                dream.Sponsors = request.SponsorIds.Select(v => new Database.Models.AssignedSponsor { SponsorId = v }).ToList();
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateDreamResponse();
        }
    }
}