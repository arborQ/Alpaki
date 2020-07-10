using System;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using GraphQL;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class DreamerSchema : Schema
    {
        public DreamerSchema(IDependencyResolver resolver, ICurrentUserService currentUserService)
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
                throw new UnauthorizedAccessException("Only authorized users can access GraphQL");
            }
        }
    }
}
