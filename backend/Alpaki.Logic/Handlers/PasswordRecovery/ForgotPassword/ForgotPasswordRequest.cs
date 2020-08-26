using MediatR;

namespace Alpaki.Logic.Handlers.PasswordRecovery.ForgotPassword
{
    public class ForgotPasswordRequest : IRequest<ForgotPasswordResponse>
    {
        public string Email { get; set; }
    }
}