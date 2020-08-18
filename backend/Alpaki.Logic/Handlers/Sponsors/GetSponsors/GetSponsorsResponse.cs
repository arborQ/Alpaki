using System.Collections.Generic;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsors
{
    public class GetSponsorsResponse
    {
        public List<SponsorItem> Sponsors { get; set; }

        public class SponsorItem
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string PhoneNumber { get; set; }
            public string Mail { get; set; }
        }
    }
}