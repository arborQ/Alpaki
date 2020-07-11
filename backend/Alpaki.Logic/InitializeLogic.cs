using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services)
        {
            services.AddScoped<InvitationUniqueCodesGenerator>();
            return services;
        }
    }
}
