using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var userRoles = new[] {UserRoleEnum.Coordinator, UserRoleEnum.Volunteer};
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId && userRoles.Contains(x.Role), cancellationToken);

            if (user is null)
            {
                throw new InvalidUserException();
            }

            user.Role = request.Role;
            
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new ChangeUserRoleResponse();
        }
    }
}