using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Alpaki.Logic.Handlers.GetUsers.GetUsersResponse;

namespace Alpaki.Logic.Handlers.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersHandler(IDatabaseContext databaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _currentUserService = currentUserService;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var query = ResolveUserQuery();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.FirstName.Contains(request.Search) || u.LastName.Contains(request.Search) || u.Email.Contains(request.Search));
            }

            if (request.Page.HasValue)
            {
                query = query.Paged(request.Page.Value);
            }

            query = query.OrderByProperty(request.OrderBy, request.Asc);

            var userList = await query.Select(UserListItem.UserToUserListItemMapper).ToListAsync();

            return new GetUsersResponse { Users = userList };
        }

        private IQueryable<User> ResolveUserQuery()
        {
            if (!_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Coordinator))
            {
                var currentUserId = _currentUserService.CurrentUserId;
                var dreamIds = _databaseContext.AssignedDreams.Where(ad => ad.VolunteerId == currentUserId).Select(ad => ad.DreamId);

                return _databaseContext.Users.Where(u => u.UserId == currentUserId || u.AssignedDreams.Any(ad => dreamIds.Contains(ad.DreamId)));
            }

            return _databaseContext.Users;
        }
    }
}
