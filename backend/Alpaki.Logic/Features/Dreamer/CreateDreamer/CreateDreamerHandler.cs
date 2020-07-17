using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;

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
            var newDream = new Database.Models.Dream
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                DreamUrl = request.DreamUrl,
                Gender = request.Gender,
                DreamCategoryId = request.CategoryId,
                Tags = request.Tags,
                DreamState = DreamStateEnum.Created
            };

            await _databaseContext.Dreams.AddAsync(newDream);
            await _databaseContext.SaveChangesAsync();

            return new CreateDreamerResponse { DreamerId = newDream.DreamId };
        }
    }
}
