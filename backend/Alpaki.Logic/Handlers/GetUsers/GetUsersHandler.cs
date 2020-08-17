using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetUsers.GetUsersResponse;

namespace Alpaki.Logic.Handlers.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IUserScopedDatabaseReadContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersHandler(IUserScopedDatabaseReadContext databaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _currentUserService = currentUserService;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var query = _databaseContext.Users;

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.FirstName.Contains(request.Search) || u.LastName.Contains(request.Search) || u.Email.Contains(request.Search) || u.Brand.Contains(request.Search));
            }

            if (request.DreamId.HasValue)
            {
                query = query.Where(u => u.AssignedDreams.Any(ad => ad.DreamId == request.DreamId.Value));
            }

            if (request.Page.HasValue)
            {
                query = query.Paged(request.Page.Value);
            }

            query = query.OrderByProperty(request.OrderBy, request.Asc);

            var userList = await query.Select(UserListItem.UserToUserListItemMapper).ToListAsync();

            return new GetUsersResponse { Users = userList };
        }
    }
}
