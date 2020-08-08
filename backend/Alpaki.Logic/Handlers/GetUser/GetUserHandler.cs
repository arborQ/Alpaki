using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserScopedDatabaseReadContext _userScopedDatabaseReadContext;

        public GetUserHandler(IUserScopedDatabaseReadContext userScopedDatabaseReadContext)
        {
            _userScopedDatabaseReadContext = userScopedDatabaseReadContext;
        }

        public Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            return _userScopedDatabaseReadContext.Users
                .Select(GetUserResponse.UserToGetUserResponseMapper)
                .SingleOrDefaultAsync(u => u.UserId == request.UserId);
        }
    }
}
