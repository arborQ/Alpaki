using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public GetUserHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            return _databaseContext
                .Users
                .Select(GetUserResponse.UserToGetUserResponseMapper)
                .Where(u => u.UserId == request.UserId)
                .SingleAsync();
        }
    }
}
