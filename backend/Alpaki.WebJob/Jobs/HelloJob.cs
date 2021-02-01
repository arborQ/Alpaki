using System;
using System.Threading.Tasks;
using Quartz;

namespace Alpaki.WebJob.Jobs
{
    public class HelloJob : IScheduledJob
    {
        public string Name => nameof(HelloJob);

        public string GroupName => $"{nameof(HelloJob)}Group";
        public Action<SimpleScheduleBuilder> Schedule => x => x.WithIntervalInSeconds(1).WithRepeatCount(20);

        public Task Execute(IJobExecutionContext context)
        {
            return Console.Out.WriteLineAsync("Work work :)");
        }
    }
}