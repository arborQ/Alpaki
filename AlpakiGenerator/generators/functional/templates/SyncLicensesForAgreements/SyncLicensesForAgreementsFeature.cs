using System;
using System.Linq;
using System.Net;
using Pyra.Csp.LicenseAssignments.FakeApi.Fakes.Emerald;
using Pyra.Csp.LicenseAssignments.Infrastructure.Clients.PartnerNetwork.Partner.Client;
using Pyra.Csp.LicenseAssignments.Tests.Integration.Fixtures;
using Pyra.Csp.LicenseAssignments.Tests.Integration.Helpers;
using Xbehave;
using Xunit;

namespace Pyra.Csp.LicenseAssignments.Tests.Functional.SyncLicensesForAgreements
{
    public partial class SyncLicensesForAgreementsFeature
    {
        [Scenario(DisplayName = "Sync Licenses For Agreements")]
        public void NotifySalesAboutIncomingRenewals(ApiResponse<EmptyResponse> httpResponseMessage)
        {
            var cspAgreementIds = EmeraldFakeService.MicrosoftCspAgreements.Select(cs => cs.MicrosoftCspAgreementId).Distinct().ToList();
            "Settup data".x(() =>
            {
                foreach (var cspAgreementId in cspAgreementIds)
                {
                    FakePartnerClientFactory.UsageDictionary.Add(
                        cspAgreementId,
                        new[] {
                            new ProductUsage
                            { 
                                CustomerTenantId = Guid.NewGuid(), 
                                LicenseActive = 10, 
                                LicensesQualified = 20, 
                                ProductId = Guid.NewGuid().ToString(), 
                                ProductName = "Product name"
                            }
                        });
                }
            });

            "When SyncLicensesForAgreements feature is triggered via API".x(
                async () =>
                {
                    httpResponseMessage = await _webJobFixture.InternalApiIntegrationTestsFixture.PostDataAsync<EmptyResponse>(
                        $"/maintenance/SyncLicensesForAgreements", new { });
                });

            "Assert status code".x(() =>
            {
                Assert.Equal(HttpStatusCode.NoContent, httpResponseMessage.HttpStatusCode);
            });

            "Wait for results".x(async () =>
            {
                await WaitForResults(cspAgreementIds);
            });
        }
    }
}
