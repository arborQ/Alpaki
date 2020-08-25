using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.AddSponsor
{
    public class AddSponsorRequest : IRequest<AddSponsorResponse>
    {
        public string DisplayName { get; set; }

        public string ContactPersonName { get; set; }

        public string Email { get; set; }

        public string Brand { get; set; }

        public SponsorTypeEnum CooperationType { get; set; }
    }
}
