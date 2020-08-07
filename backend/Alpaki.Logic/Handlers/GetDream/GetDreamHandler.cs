using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetDreams.GetDreamResponse;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamHandler : IRequestHandler<GetDreamRequest, GetDreamResponse>
    {
        private readonly IUserScopedDatabaseReadContext _databaseContext;

        public GetDreamHandler(IUserScopedDatabaseReadContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<GetDreamResponse> Handle(GetDreamRequest request, CancellationToken cancellationToken)
        {
            return _databaseContext.Dreams
                .Select(DreamToDreamListItemMapper)
                .SingleAsync(d => d.DreamId == request.DreamId);
        }
    }
}
