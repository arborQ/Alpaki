using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Alpaki.Logic.Mails
{
    internal class FakeMailService : IMailService
    {
        private readonly ILogger<FakeMailService> _logger;

        public FakeMailService(ILogger<FakeMailService> logger)
        {
            _logger = logger;
        }

        public Task Send(MimeMessage message)
        {
            _logger.LogInformation("[FakeMailService] send email", message);

            return Task.CompletedTask;
        }
    }
}