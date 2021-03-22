using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.SearchEngine
{
    public static class SearchEngineModuleInstaller
    {
        public static IServiceCollection InstallSearchEngine(this IServiceCollection serviceCollection, SearchClientConfiguration searchClientConfiguration)
        {
            serviceCollection.AddSingleton(searchClientConfiguration);
            serviceCollection.AddTransient<ISearchWriteClient, SearchWriteClient>();

            return serviceCollection;
        }
    }
}
