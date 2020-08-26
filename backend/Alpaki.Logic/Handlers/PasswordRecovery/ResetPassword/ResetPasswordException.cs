namespace Alpaki.Logic.Handlers.PasswordRecovery.ResetPassword
{
    public class ResetPasswordException : LogicException
    {
        public override string Code => "PASSWORD_RESET_FAILED";

        public ResetPasswordException() : base($"Password could not be reset.")
        {
        }
    }
}