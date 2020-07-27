namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class InvalidPasswordException : LogicException
    {
        public InvalidPasswordException() : base("Invalid password")
        {
        }

        public override string Code => "INVALID_PASSWORD";
    }
}
