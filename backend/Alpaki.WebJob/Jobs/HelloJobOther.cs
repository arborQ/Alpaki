using System;
using System.Threading.Tasks;
using Quartz;

namespace Alpaki.WebJob.Jobs
{
    public class HelloJobOther : IScheduledJob
    {
        public string Name => nameof(HelloJobOther);

        public string GroupName => $"{nameof(HelloJobOther)}Group";
        public Action<SimpleScheduleBuilder> Schedule => x => x.WithIntervalInSeconds(5).WithRepeatCount(2);

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("OTHER Greetings from HelloJob!!!!!");
        }
    }
}