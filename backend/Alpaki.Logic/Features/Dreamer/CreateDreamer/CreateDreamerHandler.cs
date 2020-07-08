using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerHandler : IRequestHandler<CreateDreamerRequest, CreateDreamerResponse>
    {
        private readonly CreateDreamerRequestValidator _validationRules;
        private readonly DatabaseContext _databaseContext;

        public CreateDreamerHandler(CreateDreamerRequestValidator validationRules, DatabaseContext databaseContext)
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

            var newDreamer = new Database.Models.Dreamer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                DreamUrl = request.DreamUrl,
                Gender = request.Gender
            };

            await _databaseContext.Dreamers.AddAsync(newDreamer);
            await _databaseContext.SaveChangesAsync();

            return new CreateDreamerResponse { DreamerId = newDreamer.DreamerId };
        }
    }
}
