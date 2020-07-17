using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDatabaseContext _databaseContext;

        public DeleteUserHandler(ICurrentUserService currentUserService, IDatabaseContext databaseContext)
        {
            _currentUserService = currentUserService;
            _databaseContext = databaseContext;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userToRemove = await _databaseContext.Users.SingleAsync(u => u.UserId == request.UserId);
            _databaseContext.Users.Remove(userToRemove);
            await _databaseContext.SaveChangesAsync();

            return new DeleteUserResponse();
        }
    }
}
