using System.Collections.Generic;
using System.Linq;
using Alpaki.Database;
using Alpaki.Logic.Expressions;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamerQuery : ObjectGraphType<DreamType>
    {
        private readonly IDreamersExpressions _volontierDreamerExpressions;
        private readonly IUserExpressions _volontierUserExpressions;

        public DreamerQuery(DatabaseContext databaseContext, DatabaseContext userDatabaseContext, VolontierDreamerExpressions volontierDreamerExpressions, VolontierUserExpressions volontierUserExpressions)
        {
            Name = "DreamQuery";
            var arguments = new QueryArguments(new List<QueryArgument>
            {
                new QueryArgument<IdGraphType> { Name = "dreamId" },
                new QueryArgument<StringGraphType> { Name = "searchName" }
            });

            var userArguments = new QueryArguments(new List<QueryArgument>
            {
                new QueryArgument<IdGraphType> { Name = "userId" },
            });

            Field<ListGraphType<UserType>>("users", arguments: userArguments, resolve: context =>
            {
                var userQuery = userDatabaseContext.Users.Where(_volontierUserExpressions.UserQuery);

                var userId = context.GetArgument<int?>("userId");

                if (userId.HasValue)
                {
                    return userQuery.Where(u => u.UserId == userId).ToListAsync();
                }

                return userQuery.ToListAsync();
            });

            Field<ListGraphType<DreamType>>("dreams", arguments: arguments, resolve: context =>
            {
                var dreamerQuery = databaseContext.Dreams
                    .Include(d => d.DreamCategory)
                    .Include(d => d.RequiredSteps)
                    .Where(_volontierDreamerExpressions.DreamersQuery);

                var dreamerId = context.GetArgument<int?>("dreamId");

                if (dreamerId.HasValue)
                {
                    return dreamerQuery.Where(d => d.DreamId == dreamerId).ToListAsync();
                }

                var searchName = context.GetArgument<string>("searchName");

                if (!string.IsNullOrEmpty(searchName))
                {
                    return dreamerQuery.Where(d => (d.FirstName + d.LastName).Contains(searchName)).ToListAsync();
                }

                return dreamerQuery.ToListAsync();
            });
            _volontierDreamerExpressions = volontierDreamerExpressions;
            _volontierUserExpressions = volontierUserExpressions;
        }
    }
}
