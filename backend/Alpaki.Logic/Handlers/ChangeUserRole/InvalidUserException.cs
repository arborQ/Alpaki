namespace Alpaki.Logic.Handlers.ChangeUserRole
{
    public class InvalidUserException : LogicException
    {
        public override string Code => "invalid_user_exception";

        public InvalidUserException() : base($"Podany użytkownik jest niepoprawny.")
        {
        }
    }
}