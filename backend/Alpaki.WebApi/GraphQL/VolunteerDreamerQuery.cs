using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;

namespace Alpaki.WebApi.GraphQL
{
    public class VolunteerDreamerQuery : DreamerQuery
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DatabaseContext _userDatabaseContext;
        private readonly ICurrentUserService _currentUserService;

        public VolunteerDreamerQuery(DatabaseContext databaseContext, DatabaseContext userDatabaseContext, ICurrentUserService currentUserService)
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
            return _databaseContext.Users.Where(u => u.UserId == _currentUserService.CurrentUserId);
        }
    }
}
