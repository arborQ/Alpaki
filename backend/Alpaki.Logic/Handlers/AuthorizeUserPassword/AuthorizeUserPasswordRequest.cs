using MediatR;

namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class AuthorizeUserPasswordRequest : IRequest<AuthorizeUserPasswordResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
