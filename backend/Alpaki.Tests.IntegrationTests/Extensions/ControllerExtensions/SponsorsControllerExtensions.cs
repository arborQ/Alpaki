using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Handlers.Sponsors.GetSponsors;
using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;

namespace Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions
{
    internal static class SponsorsControllerExtensions
    {
        internal static async Task<AddSponsorResponse> AddSponsor(this HttpClient client, AddSponsorRequest request)
        {
            var response = await client.PostAsync("/api/sponsors", request.AsJsonContent());
            response.EnsureSuccessStatusCode();
            return await response.GetResponse<AddSponsorResponse>();
        }
        internal static Task<GetSponsorsResponse> GetSponsors(this HttpClient client)
        {
            return client.GetAsync($"/api/sponsors").AsResponse<GetSponsorsResponse>();
        }
        internal static Task<SponsorItem> GetSponsor(this HttpClient client, long sponsorId)
        {
            return client.GetAsync($"/api/sponsors/{sponsorId}").AsResponse<SponsorItem>();
        }
        internal class AddSponsorRequest
        {
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string PhoneNumber { get; set; }
            public string Mail { get; set; }
            public string Brand { get; set; }
            public SponsorCooperationEnum CooperationType { get; set; }
        }
    
        internal class AddSponsorResponse
        {
            public long SponsorId { get; set; }
        }
    }
    
}