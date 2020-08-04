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
            var dream = await _dbContext.Dreams.SingleOrDefaultAsync(x => x.DreamId == request.DreamId, cancellationToken);
            if(dream is null)
                throw new DreamNotFoundException(request.DreamId);

            if (request.FirstName is {})
                dream.FirstName = request.FirstName;
            if (request.LastName is {})
                dream.LastName = request.LastName;
            if (request.Age.HasValue)
                dream.Age = request.Age.Value;
            if (request.Gender.HasValue)
                dream.Gender = request.Gender.Value;
            if (request.DreamUrl is {})
                dream.DreamUrl = request.DreamUrl;
            if (request.Tags is {})
                dream.Tags = request.Tags;
            if (request.DreamCategoryId.HasValue) 
                dream.DreamCategoryId = request.DreamCategoryId.Value;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateDreamerResponse();
        }
    }
}