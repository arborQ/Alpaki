using Alpaki.CrossCutting.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Alpaki.WebApi.Policies
{
    public class ModuleAccessAttribute : AuthorizeAttribute
    {
        public static string ResolvePolicyName(ApplicationType applicationType) => $"{applicationType}AccessPolicy";

        public ModuleAccessAttribute(ApplicationType applicationType) : base(ResolvePolicyName(applicationType)) { }
    }
}
