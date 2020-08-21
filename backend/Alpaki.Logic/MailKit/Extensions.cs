
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic.Mails
{
    public static class Extensions
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services)
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                var mailSenderSection = configuration.GetSection("MailSender").Get<MailKitOptions>();

                if (string.IsNullOrEmpty(mailSenderSection.Host))
                {
                    services.AddSingleton<IMailService, FakeMailService>();
                }
                else
                {
                    services.AddSingleton<IMailService, MailService>();
                }

                services.Configure<MailKitOptions>(options => configuration.GetSection("MailSender").Bind(options));
            }

            return services;
        }
    }
}