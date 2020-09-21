using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Jobs;

namespace Alpaki.Moto.Job.Handlers
{

    public class BrandReader : QueueReader<BrandMessage>, IJobHandler
    {
        protected override async Task<bool> HandleMessageProcess(BrandMessage messageBody, CancellationToken token)
        {
            await Console.Out.WriteLineAsync("Message handle!!!!!!" + messageBody.BrandName);

            return true;
        }
    }
}
