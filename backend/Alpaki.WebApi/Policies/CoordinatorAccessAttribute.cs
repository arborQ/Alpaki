using Microsoft.AspNetCore.Authorization;

namespace Alpaki.WebApi.Policies
{
    public class CoordinatorAccessAttribute : AuthorizeAttribute
    {
        public const string PolicyName = "CoordinatorAccess";

        public CoordinatorAccessAttribute() : base(PolicyName) { }
    }
}
