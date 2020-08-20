using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.AddSponsor
{
    public class AddSponsorRequest : IRequest<AddSponsorResponse>
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Brand { get; set; }
        public SponsorCooperationEnum CooperationType { get; set; }
    }
}