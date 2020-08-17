using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerHandler : IRequestHandler<CreateDreamerRequest, CreateDreamerResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public CreateDreamerHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<CreateDreamerResponse> Handle(CreateDreamerRequest request, CancellationToken cancellationToken)
        {
            var requiredSteps = await _databaseContext.DreamCategoryDefaultSteps.Where(s => s.DreamCategoryId == request.CategoryId).ToListAsync();

            var newDream = new Database.Models.Dream
            {
                DisplayName = request.DisplayName,
                Age = request.Age,
                DreamUrl = request.DreamUrl,
                DreamCategoryId = request.CategoryId,
                Tags = request.Tags,
                DreamState = DreamStateEnum.Created,
                RequiredSteps = requiredSteps
                    .Select(s => new Database.Models.DreamStep
                    {
                        StepDescription = s.StepDescription,
                        StepState = !request.IsSponsorRequired && s.IsSponsorRelated? StepStateEnum.Skiped : StepStateEnum.Awaiting
                    })
                    .ToList()
            };

            await _databaseContext.Dreams.AddAsync(newDream);
            await _databaseContext.SaveChangesAsync();

            return new CreateDreamerResponse { DreamerId = newDream.DreamId };
        }
    }
}
