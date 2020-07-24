namespace Alpaki.CrossCutting.Interfaces
{
    public interface IJwtGenerator
    {
        string Generate(IUser user);
    }
}