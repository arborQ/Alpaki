using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.RemoveSponsor
{
    public class RemoveSponsorRequest : IRequest<RemoveSponsorResponse>
    {
        public long Id { get; set; }
    }
}