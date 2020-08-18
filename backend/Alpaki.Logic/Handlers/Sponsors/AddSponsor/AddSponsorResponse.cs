namespace Alpaki.Logic.Handlers.Sponsors.AddSponsor
{
    public class AddSponsorResponse
    {
        public long SponsorId { get; }

        public AddSponsorResponse(long sponsorId)
        {
            SponsorId = sponsorId;
        }
    }
}