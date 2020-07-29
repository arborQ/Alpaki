namespace Alpaki.Logic.Handlers.DeleteUser
{
    public class UserNotFoundException : LogicException
    {
        public UserNotFoundException(long userId) 
            : base($"User with given UserId does not exists [UserId={userId}]")
        {
        }

        public override string Code => "USER_NOT_FOUND";
    }
}
