using Alpaki.Logic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}
