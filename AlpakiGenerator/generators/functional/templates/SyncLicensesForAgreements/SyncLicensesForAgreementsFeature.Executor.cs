using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Pyra.Csp.LicenseAssignments.Tests.Functional.TestCollections;
using Xunit;

namespace Pyra.Csp.LicenseAssignments.Tests.Functional.SyncLicensesForAgreements
{
    [Collection(nameof(WebJobWithInternalApiFunctionalTests))]
    public partial class SyncLicensesForAgreementsFeature
    {
        private readonly WebJobFixture _webJobFixture;

        public SyncLicensesForAgreementsFeature(WebJobFixture webJobFixture)
        {
            _webJobFixture = webJobFixture;
        }

        async Task WaitForResults(IReadOnlyCollection<Guid> cspAgreementIds)
        {
            var sqlConnection = _webJobFixture.SqlConnection;

            for (var i = 0; i < 20; i++)
            {
                var count = await sqlConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [ProductUsage] WHERE [CspAgreementId] IN @cspAgreementIds", new { cspAgreementIds });

                if (count > 0)
                {
                    return;
                }
                await Task.Delay(1000);
            }

            throw new Exception("Usage records not found");
        }
    }
}
