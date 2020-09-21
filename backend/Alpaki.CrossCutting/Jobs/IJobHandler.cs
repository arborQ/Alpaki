using System.Threading;
using System.Threading.Tasks;

namespace Alpaki.CrossCutting.Jobs
{
    public interface IJobHandler
    {
        Task WaitForMessages(CancellationToken token);
    }
}
