using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AddDream
{
    public class AddDreamHandler : IRequestHandler<AddDreamRequest, AddDreamResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public AddDreamHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<AddDreamResponse> Handle(AddDreamRequest request, CancellationToken cancellationToken)
        {
            var requiredSteps = await _databaseContext.DreamCategoryDefaultSteps.Where(s => s.DreamCategoryId == request.CategoryId).ToListAsync();

            var newDream = new Database.Models.Dream
            {
                Title =  request.Title,
                DisplayName = request.DisplayName,
                Age = request.Age,
                DreamUrl = request.DreamUrl,
                DreamCategoryId = request.CategoryId,
                Tags = request.Tags,
                DreamState = DreamStateEnum.Created,
                DreamImageId = request.DreamImageId,
                Volunteers = request.VolunteerIds.Select(v => new Database.Models.AssignedDreams { VolunteerId = v }).ToList(),
                RequiredSteps = requiredSteps
                    .Select(s => new Database.Models.DreamStep
                    {
                        StepDescription = s.StepDescription,
                        StepState = !request.IsSponsorRequired && s.IsSponsorRelated ? StepStateEnum.Skiped : StepStateEnum.Awaiting
                    })
                    .ToList()
            };

            await _databaseContext.Dreams.AddAsync(newDream, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return new AddDreamResponse { DreamId = newDream.DreamId };
        }
    }
}
