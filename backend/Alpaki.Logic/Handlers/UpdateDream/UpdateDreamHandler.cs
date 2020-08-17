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

            if (!string.IsNullOrEmpty(request.Tags))
            {
                dream.Tags = request.Tags;
            }

            if (request.DreamCategoryId.HasValue)
            {
                dream.DreamCategoryId = request.DreamCategoryId.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateDreamResponse();
        }
    }
}