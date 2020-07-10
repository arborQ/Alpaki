using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerHandler : IRequestHandler<CreateDreamerRequest, CreateDreamerResponse>
    {
        private readonly CreateDreamerRequestValidator _validationRules;
        private readonly IDatabaseContext _databaseContext;

        public CreateDreamerHandler(CreateDreamerRequestValidator validationRules, IDatabaseContext databaseContext)
        {
            _validationRules = validationRules;
            _databaseContext = databaseContext;
        }

        public async Task<CreateDreamerResponse> Handle(CreateDreamerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validationRules.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                // TODO: provide valid exception
                throw new System.Exception("Invalid request");
            }

            var newDream = new Database.Models.Dream
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                DreamUrl = request.DreamUrl,
                Gender = request.Gender,
                DreamCategoryId = 1,
                Tags = "#MM",
                DreamState = DreamStateEnum.Created
            };

            await _databaseContext.Dreams.AddAsync(newDream);
            await _databaseContext.SaveChangesAsync();

            return new CreateDreamerResponse { DreamerId = newDream.DreamId };
        }
    }
}
