using System.Collections.Generic;
using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsors
{
    public class GetSponsorsResponse
    {
        public List<SponsorItem> Sponsors { get; set; }
    }
}