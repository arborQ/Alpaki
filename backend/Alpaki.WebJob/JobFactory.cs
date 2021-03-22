using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Alpaki.WebJob
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;
        private ConcurrentDictionary<Type, IServiceScope> _scopes = new ConcurrentDictionary<Type, IServiceScope>();

        public JobFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;

            // MA - Generate a scope for the job, this allows the job to be registered
            //	using .AddScoped<T>() which means we can use scoped dependencies 
            //	e.g. database contexts
            var scope = _scopes.GetOrAdd(jobType, t => _container.CreateScope());

            return (IJob)scope.ServiceProvider.GetRequiredService(jobType);
        }

        public void ReturnJob(IJob job)
        {
            var jobType = job?.GetType();

            if (job != null && _scopes.TryGetValue(jobType, out var scope))
            {
                //  MA - Dispose of the scope, which disposes of the job's dependencies
                scope.Dispose();

                // MA - Remove the scope so the next time the job is resolved, 
                //	we can get a new job instance
                _scopes.TryRemove(jobType, out _);
            }
        }
    }
}
