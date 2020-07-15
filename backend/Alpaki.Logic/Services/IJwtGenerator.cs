using Alpaki.Database.Models;

namespace Alpaki.Logic.Services
{
    public interface IJwtGenerator
    {
        string Generate(User user);
    }
}