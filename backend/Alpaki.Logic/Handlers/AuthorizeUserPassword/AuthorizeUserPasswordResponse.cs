using Alpaki.CrossCutting.Enums;

namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class AuthorizeUserPasswordResponse
    {
        public string Token { get; set; }

        public string Login { get; set; }

        public ApplicationType ApplicationType { get; set; }
    }
}
