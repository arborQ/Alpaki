using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsor
{
    public class GetSponsorRequest : IRequest<SponsorItem>
    {
        public long SponsorId { get; set; }
    }
}