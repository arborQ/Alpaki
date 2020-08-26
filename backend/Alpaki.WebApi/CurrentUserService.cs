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

        public long CurrentUserId
        {
            get
            {
                try
                {
                    return long.Parse(_httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value);
                }
                catch
                {
                    return default;
                }
            }
        }

        public UserRoleEnum CurrentUserRole
        {
            get
            {
                try
                {
                    return (UserRoleEnum)int.Parse(_httpContext.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value);
                }
                catch
                {
                    return UserRoleEnum.None;
                }
            }
        }
    }
}
