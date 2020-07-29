namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class InvalidPasswordException : LogicException
    {
        public InvalidPasswordException() : base("Niepoprawne hasło")
        {
        }

        public override string Code => "INVALID_PASSWORD";
    }
}
