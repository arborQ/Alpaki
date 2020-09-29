using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Common.Jobs.Models;

namespace Alpaki.Common.Jobs
{
    public interface IJobSubscriber
    {
        Task Subscribe(Func<StringBusinessEventModel, Task> eventAction, string eventType, CancellationToken cancellationToken);
    }
}
