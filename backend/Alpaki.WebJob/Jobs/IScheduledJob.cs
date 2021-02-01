using System;
using Quartz;

namespace Alpaki.WebJob.Jobs
{
    public interface IScheduledJob : IJob
    {
        string Name { get; }

        string GroupName { get; }

        Action<SimpleScheduleBuilder> Schedule { get; }
    }
}