using Alpaki.CrossCutting.Enums;

namespace Alpaki.CrossCutting.Interfaces
{
    public interface ICurrentUserService
    {
        long CurrentUserId { get; }

        UserRoleEnum CurrentUserRole { get; }

        ApplicationType ApplicationType { get; }
    }
}
