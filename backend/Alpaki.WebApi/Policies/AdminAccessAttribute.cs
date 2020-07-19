using Microsoft.AspNetCore.Authorization;

namespace Alpaki.WebApi.Policies
{
    public class AdminAccessAttribute : AuthorizeAttribute
    {
        public const string PolicyName = "AdminAccessPolicy";

        public AdminAccessAttribute() : base(PolicyName) { }
    }
}
