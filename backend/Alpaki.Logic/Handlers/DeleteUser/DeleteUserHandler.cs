using System.Linq;
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
            var userToRemove = _databaseContext.Users.Where(u => u.UserId == request.UserId);
            var assignedDreamsConnections = _databaseContext.AssignedDreams.Where(a => a.VolunteerId == request.UserId);

            _databaseContext.AssignedDreams.RemoveRange(assignedDreamsConnections);
            _databaseContext.Users.RemoveRange(userToRemove);

            await _databaseContext.SaveChangesAsync();

            return new DeleteUserResponse();
        }
    }
}
