using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.WebJob.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Alpaki.WebJob
{
    public class Worker : BackgroundService
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

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            foreach (var job in _jobs)
            {
                _logger.LogInformation("Add job Name=[{name}] at: {time}", job.Name, DateTimeOffset.Now);

                var scheduledJob = JobBuilder.Create(job.GetType())
                    .WithIdentity(job.Name, job.GroupName)
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", job.GroupName)
                    .WithSimpleSchedule(job.Schedule)
                    .Build();

                await _scheduler.ScheduleJob(scheduledJob, trigger, cancellationToken);
            }

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Start(cancellationToken);
            _logger.LogInformation("Scheduler started at: {time}", DateTimeOffset.Now);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.Shutdown(cancellationToken);
                _logger.LogInformation("Scheduler shutdown at: {time}", DateTimeOffset.Now);
            }

            await base.StopAsync(cancellationToken);
        }
    }
}
