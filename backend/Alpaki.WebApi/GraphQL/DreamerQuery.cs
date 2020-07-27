using System.Collections.Generic;
using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Extensions;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.WebApi.GraphQL
{
    public abstract class DreamerQuery : ObjectGraphType<DreamType>
    {
        protected abstract IQueryable<Dream> QueryDreams();

        protected abstract IQueryable<User> QueryUsers();

        protected abstract IQueryable<Invitation> QueryInvitations();

        public DreamerQuery()
        {
            Name = "DreamQuery";
            var arguments = new QueryArguments(new List<QueryArgument>
            {
                new QueryArgument<IdGraphType> { Name = "dreamId" },
                new QueryArgument<StringGraphType> { Name = "searchName" },
                new QueryArgument<GenderEnumType> { Name = "gender" },
                new QueryArgument<DreamStateEnumType> { Name = "status" },
                new QueryArgument<IntGraphType> { Name = "ageFrom" },
                new QueryArgument<IntGraphType> { Name = "ageTo" },
                new QueryArgument<ListGraphType<IntGraphType>> { Name = "categories" },
                new QueryArgument<StringGraphType> { Name = "orderBy", DefaultValue = "DreamId" },
                new QueryArgument<BooleanGraphType> { Name = "orderAsc", DefaultValue = true }
            });

            var userArguments = new QueryArguments(new List<QueryArgument>
            {
                new QueryArgument<IdGraphType> { Name = "userId" },
                new QueryArgument<IdGraphType> { Name = "dreamId" },
                new QueryArgument<StringGraphType> { Name = "orderBy", DefaultValue = "UserId" },
                new QueryArgument<BooleanGraphType> { Name = "orderAsc", DefaultValue = true }
            });

            Field<ListGraphType<UserType>>("users", arguments: userArguments, resolve: context =>
            {
                var orderBy = context.GetArgument<string>("orderBy");
                var orderAsc = context.GetArgument<bool>("orderAsc");

                var userQuery = QueryUsers()
                    .OrderByProperty(orderBy, orderAsc);

                var userId = context.GetArgument<int?>("userId");

                if (userId.HasValue)
                {
                    userQuery = userQuery.Where(u => u.UserId == userId);
                }

                var dreamId = context.GetArgument<int?>("dreamId");

                if (dreamId.HasValue)
                {
                    userQuery = userQuery.Where(u => u.AssignedDreams.Any(ad => ad.DreamId == dreamId.Value));
                }

                return userQuery.ToListAsync();
            });

            Field<ListGraphType<DreamType>>("dreams", arguments: arguments, resolve: context =>
            {
                var gender = context.GetArgument<GenderEnum?>("gender");
                var status = context.GetArgument<DreamStateEnum?>("status");
                var ageFrom = context.GetArgument<int?>("ageFrom");
                var ageTo = context.GetArgument<int?>("ageTo");
                var categoryIds = context.GetArgument<List<long>>("categories");
                var orderBy = context.GetArgument<string>("orderBy");
                var orderAsc = context.GetArgument<bool>("orderAsc");

                var dreamerQuery = QueryDreams()
                    .Include(d => d.DreamCategory)
                    .Include(d => d.RequiredSteps)
                    .OrderByProperty(orderBy, orderAsc);

                var dreamerId = context.GetArgument<int?>("dreamId");

                if (dreamerId.HasValue)
                {
                    return dreamerQuery.Where(d => d.DreamId == dreamerId).ToListAsync();
                }

                if (gender.HasValue)
                {
                    dreamerQuery = dreamerQuery.Where(d => d.Gender == gender.Value);
                }

                if (status.HasValue)
                {
                    dreamerQuery = dreamerQuery.Where(d => d.DreamState == status.Value);
                }

                if (ageFrom.HasValue)
                {
                    dreamerQuery = dreamerQuery.Where(d => d.Age >= ageFrom.Value);
                }

                if (ageTo.HasValue)
                {
                    dreamerQuery = dreamerQuery.Where(d => d.Age <= ageTo.Value);
                }

                if (categoryIds != null && categoryIds.Any())
                {
                    dreamerQuery = dreamerQuery.Where(d => categoryIds.Contains(d.DreamCategoryId));
                }

                var searchName = context.GetArgument<string>("searchName");

                if (!string.IsNullOrEmpty(searchName))
                {
                    dreamerQuery = dreamerQuery.Where(d => (d.FirstName + d.LastName).Contains(searchName));
                }

                return dreamerQuery.ToListAsync();
            });

            Field<ListGraphType<InvitationType>>(
                "invitations",
                resolve: context => QueryInvitations().ToListAsync()
            );
        }
    }
}
