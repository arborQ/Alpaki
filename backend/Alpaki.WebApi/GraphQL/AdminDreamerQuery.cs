using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;

namespace Alpaki.WebApi.GraphQL
{
    public class AdminDreamerQuery : DreamerQuery
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IDatabaseContext _userDatabaseContext;

        public AdminDreamerQuery(IDatabaseContext databaseContext, IDatabaseContext userDatabaseContext)
        {
            _databaseContext = databaseContext;
            _userDatabaseContext = userDatabaseContext;
        }

        protected override IQueryable<Dream> QueryDreams()
        {
            return _databaseContext.Dreams;
        }

        protected override IQueryable<User> QueryUsers()
        {
            return _userDatabaseContext.Users;
        }

        protected override IQueryable<Invitation> QueryInvitations()
        {
            return _databaseContext.Invitations.Where(x=>x.Status == InvitationStateEnum.Pending);
        }
    }
}
