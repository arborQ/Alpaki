using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Alpaki.WebApi.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IDatabaseContext _databaseContext;

        public DatabaseHealthCheck(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
