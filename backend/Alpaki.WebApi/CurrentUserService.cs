using System.Linq;
using System.Security.Claims;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Alpaki.WebApi
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public long CurrentUserId =>
            long.Parse(_httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value);

        public UserRoleEnum CurrentUserRole =>
            (UserRoleEnum) int.Parse(_httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value);
    }
}
