using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.SearchEngine
{
    public static class ModuleInstaller
    {
        public static IServiceCollection InstalSearchEngine(this IServiceCollection services, SearchClientConfiguration searchClientConfiguration)
        {
            services.AddSingleton(searchClientConfiguration);
            services.AddTransient<ISearchWriteClient, SearchWriteClient>();

            return services;
        }
    }
}
