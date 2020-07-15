using Alpaki.CrossCutting.Enums;

namespace Alpaki.Logic.Services
{
    public interface IJwtGenerator
    {
        string Generate(long userId, UserRoleEnum role);
    }
}
