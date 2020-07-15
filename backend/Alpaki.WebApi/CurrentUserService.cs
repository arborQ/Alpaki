using System.Linq;
using System.Security.Claims;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Alpaki.WebApi
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            CurrentUserId = long.Parse(httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value);
            CurrentUserRole = (UserRoleEnum)int.Parse(httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value);
        }

        public long CurrentUserId { get; }

        public UserRoleEnum CurrentUserRole { get; }
    }
}
