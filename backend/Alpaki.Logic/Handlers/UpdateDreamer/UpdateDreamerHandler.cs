using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateDreamer
{
    public class UpdateDreamerHandler : IRequestHandler<UpdateDreamerRequest, UpdateDreamerResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public UpdateDreamerHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UpdateDreamerResponse> Handle(UpdateDreamerRequest request, CancellationToken cancellationToken)
        {
            var dream = await _dbContext.Dreams.SingleAsync(x => x.DreamId == request.DreamId, cancellationToken);

            if (!string.IsNullOrEmpty(dream.FirstName))
            {
                dream.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                dream.LastName = request.LastName;
            }

            if (request.Age.HasValue)
            {
                dream.Age = request.Age.Value;
            }

            if (request.Gender.HasValue)
            {
                dream.Gender = request.Gender.Value;
            }

            if (!string.IsNullOrEmpty(request.DreamUrl))
            {
                dream.DreamUrl = request.DreamUrl;
            }

            if (!string.IsNullOrEmpty(request.Tags))
            {
                dream.Tags = request.Tags;
            }

            if (request.DreamCategoryId.HasValue)
            {
                dream.DreamCategoryId = request.DreamCategoryId.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateDreamerResponse();
        }
    }
}