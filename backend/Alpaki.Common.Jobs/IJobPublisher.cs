using System;
using System.Threading.Tasks;

namespace Alpaki.Common.Jobs
{
    public interface IJobPublisher
    {
        Task PublishBusinesEvent<T>(T eventMessage, Guid eventId);
    }
}
