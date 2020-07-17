using Microsoft.AspNetCore.Authorization;

namespace Alpaki.WebApi.Policies
{
    public class VolunteerAccessAttribute : AuthorizeAttribute
    {
        public const string PolicyName = "VolunteerAccessPolicy";

        public VolunteerAccessAttribute() : base(PolicyName) { }
    }
}
