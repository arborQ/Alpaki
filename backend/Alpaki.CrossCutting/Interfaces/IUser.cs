using Alpaki.CrossCutting.Enums;

namespace Alpaki.CrossCutting.Interfaces
{
    public interface IUser
    {
        long UserId { get; }

        UserRoleEnum Role { get; }
    }
}