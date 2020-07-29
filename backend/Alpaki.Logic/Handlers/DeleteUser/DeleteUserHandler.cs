using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public DeleteUserHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userToRemove = _databaseContext.Users.Where(u => u.UserId == request.UserId);

            if (!(await userToRemove.AnyAsync()))
            {
                throw new UserNotFoundException(request.UserId);
            }

            var assignedDreamsConnections = _databaseContext.AssignedDreams.Where(a => a.VolunteerId == request.UserId);

            _databaseContext.AssignedDreams.RemoveRange(assignedDreamsConnections);
            _databaseContext.Users.RemoveRange(userToRemove);

            await _databaseContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return new DeleteUserResponse();
        }
    }
}
