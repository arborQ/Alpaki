using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.UpdateSponsor
{
    public class UpdateSponsorRequest : IRequest<UpdateSponsorResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Brand { get; set; }
        public SponsorCooperationEnum? CooperationType { get; set; }
    }
}