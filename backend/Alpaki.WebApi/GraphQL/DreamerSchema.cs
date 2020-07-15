using System;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamerSchema : Schema
    {
        public DreamerSchema(IDependencyResolver resolver, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor)
            : base(resolver)
        {
            if (currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Coordinator))
            {
                Query = resolver.Resolve<AdminDreamerQuery>();
            }
            else if (currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Volunteer))
            {
                Query = resolver.Resolve<VolunteerDreamerQuery>();
            }
            else
            {
                Query = resolver.Resolve<VolunteerDreamerQuery>();
            }
        }
    }
}
