using System.Collections.Generic;
using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;

namespace Alpaki.WebApi.GraphQL
{
    public class VolunteerDreamerQuery : DreamerQuery
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IDatabaseContext _userDatabaseContext;
        private readonly ICurrentUserService _currentUserService;

        public VolunteerDreamerQuery(IDatabaseContext databaseContext, IDatabaseContext userDatabaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _userDatabaseContext = userDatabaseContext;
            _currentUserService = currentUserService;
        }

        protected override IQueryable<Dream> QueryDreams()
        {
            return _databaseContext.Dreams
                .Where(d => d.Volunteers.Any(v =>
                    v.VolunteerId == _currentUserService.CurrentUserId) &&
                    d.DreamState != DreamStateEnum.Done &&
                    d.DreamState != DreamStateEnum.Terminated
                );
        }

        protected override IQueryable<User> QueryUsers()
        {
            var currentUserId = _currentUserService.CurrentUserId;
            var dreamIds = _userDatabaseContext.AssignedDreams.Where(ad => ad.VolunteerId == currentUserId).Select(ad => ad.DreamId);

            return _userDatabaseContext.Users.Where(u => u.UserId == currentUserId || u.AssignedDreams.Any(ad => dreamIds.Contains(ad.DreamId)));
        }

        protected override IQueryable<Invitation> QueryInvitations()
        {
            return new List<Invitation>().AsQueryable();
        }
    }
}
