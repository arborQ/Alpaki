using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Alpaki.WebJob
{
    public static class CronHelper
    {
        public static CronScheduleBuilder EverySecond => CronScheduleBuilder.CronSchedule("* * * * * ? *");

        public static CronScheduleBuilder EveryEachSecond(int second) => CronScheduleBuilder.CronSchedule($"0/5 0 0 ? * * *");
    }

    public interface IScheduledJob : IJob
    {
        string Name { get; }

        string GroupName { get; }

        CronScheduleBuilder CronSchedule { get; }
    }

    public class HelloJob : IScheduledJob
    {
        private readonly ILogger<HelloJob> _logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            _logger = logger;
        }

        public string Name => nameof(HelloJob);

        public string GroupName => $"{nameof(HelloJob)}Group";

        public CronScheduleBuilder CronSchedule => CronHelper.EverySecond;

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Working...");

            return Task.CompletedTask;
        }
    }

    public class HelloJobOther : IScheduledJob
    {
        public string Name => nameof(HelloJobOther);

        public string GroupName => $"{nameof(HelloJobOther)}Group";

        public CronScheduleBuilder CronSchedule => CronHelper.EveryEachSecond(5);

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("OTHER Greetings from HelloJob!!!!!");
        }
    }

    public class Worker : BackgroundService, IDisposable
    {
        private readonly IScheduler _scheduler;
        private readonly IEnumerable<IScheduledJob> _jobs;
        private readonly ILogger<Worker> _logger;

        public Worker(IScheduler scheduler, IEnumerable<IScheduledJob> jobs, ILogger<Worker> logger)
        {
            _scheduler = scheduler;
            _jobs = jobs;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var job in _jobs)
            {
                _logger.LogInformation("Add job Name=[{name}] at: {time}", job.Name, DateTimeOffset.Now);

                var scheduledJob = JobBuilder.Create(job.GetType())
                    .WithIdentity(job.Name, job.GroupName)
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", job.GroupName)
                    .WithSchedule(job.CronSchedule)
                    .StartNow()
                    .Build();

                await _scheduler.ScheduleJob(scheduledJob, trigger);
            }

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Start();
            _logger.LogInformation("Scheduler started at: {time}", DateTimeOffset.Now);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.Shutdown();
                _logger.LogInformation("Scheduler shutdown at: {time}", DateTimeOffset.Now);
            }

            await base.StopAsync(cancellationToken);
        }
    }
}
