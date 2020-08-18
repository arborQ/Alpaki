
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic.Mails
{
    public static class Extensions
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services)
        {
            services.AddSingleton<IMailService, MailService>();

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                services.Configure<MailKitOptions>(options => configuration.GetSection("MailSender").Bind(options));
            }
            
            return services;
        }
    }
}