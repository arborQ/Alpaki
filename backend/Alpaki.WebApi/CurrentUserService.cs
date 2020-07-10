using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;

namespace Alpaki.WebApi
{
    /// <summary>
    /// TODO: Implement JWT and take data from token
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        public long CurrentUserId => 2;

        public UserRoleEnum CurrentUserRole => UserRoleEnum.Admin;
    }
}
