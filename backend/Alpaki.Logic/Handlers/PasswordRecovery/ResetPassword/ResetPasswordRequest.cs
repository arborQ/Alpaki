using MediatR;

namespace Alpaki.Logic.Handlers.PasswordRecovery.ResetPassword
{
    public class ResetPasswordRequest : IRequest<ResetPasswordResponse>
    {
        public long UserId { get; set; }
        
        public string Token { get; set; }
        
        public string Password { get; set; }
    }
}