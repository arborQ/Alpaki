using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;

namespace Alpaki.WebApi.GraphQL
{
    public class CoordinatorDreamerQuery : DreamerQuery
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IDatabaseContext _userDatabaseContext;
        private readonly ICurrentUserService _currentUserService;

        public CoordinatorDreamerQuery(IDatabaseContext databaseContext, IDatabaseContext userDatabaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _userDatabaseContext = userDatabaseContext;
            _currentUserService = currentUserService;
        }

        protected override IQueryable<Dream> QueryDreams()
        {
            return _databaseContext.Dreams
                .Where(d => d.Volunteers.Any(v =>
                    d.DreamState != DreamStateEnum.Done &&
                    d.DreamState != DreamStateEnum.Terminated
                ));
        }

        protected override IQueryable<User> QueryUsers()
        {
            return _userDatabaseContext.Users.Where(u => u.Role == UserRoleEnum.Volunteer); ;
        }

        protected override IQueryable<Invitation> QueryInvitations()
        {
            return _databaseContext.Invitations.Where(x => x.Status == InvitationStateEnum.Pending);
        }
    }
}
