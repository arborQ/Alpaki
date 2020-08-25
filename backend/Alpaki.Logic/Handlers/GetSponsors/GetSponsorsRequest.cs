using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.GetSponsors
{
    public class GetSponsorsRequest: IRequest<GetSponsorsResponse>
    {
        public long? DreamId { get; set; }

        public string Search { get; set; }

        public SponsorTypeEnum? CooperationType { get; set; }

    }
}
