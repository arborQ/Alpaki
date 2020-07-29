using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Alpaki.WebApi.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var measureExecution = new Stopwatch();
            measureExecution.Start();

            var response = await next();

            measureExecution.Stop();
            var timeSpan = measureExecution.Elapsed;

            _logger.LogInformation($"MediatR Handled [{typeof(TResponse).Name}] in [{timeSpan.TotalMilliseconds}ms]");

            return response;
        }
    }
}
