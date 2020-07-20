using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;
using static Alpaki.Logic.Handlers.ChangeUserRole.Exceptions;

namespace Alpaki.Logic.Handlers.ChangeUserRole
{
    public class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleRequest,ChangeUserRoleResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public ChangeUserRoleHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ChangeUserRoleResponse> Handle(ChangeUserRoleRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            if (user.Role == UserRoleEnum.Admin)
            {
                throw new UserWithInvalidRoleException();
            }

            user.Role = request.Role;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new ChangeUserRoleResponse();
        }
    }
}