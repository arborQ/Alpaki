using System.Linq;
using Alpaki.Database;
using Alpaki.Database.Models;

namespace Alpaki.WebApi.GraphQL
{
    public class AdminDreamerQuery : DreamerQuery
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DatabaseContext _userDatabaseContext;

        public AdminDreamerQuery(DatabaseContext databaseContext, DatabaseContext userDatabaseContext)
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
    }
}
