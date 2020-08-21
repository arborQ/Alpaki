using MediatR;

namespace Alpaki.Logic.Handlers.DeleteSponsor
{
    public class DeleteSponsorRequest : IRequest<DeleteSponsorResponse>
    {
        public long SponsorId { get; set; }
    }
}
