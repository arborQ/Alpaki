using Alpaki.CrossCutting.Enums;

namespace Alpaki.Logic.Handlers.Sponsors.ResponoseDtos
{
    public class SponsorItem
    {
        public long SponsorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Brand { get; set; }
        public SponsorCooperationEnum? CooperationType { get; set; }
    }
}