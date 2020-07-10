using MediatR;

namespace Alpaki.Logic.Services
{
    public class UserAuthorizeRequest : IRequest<UserAuthorizeResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
