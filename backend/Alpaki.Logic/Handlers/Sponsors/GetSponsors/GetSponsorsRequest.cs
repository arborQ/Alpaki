using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Requests;
using Alpaki.Logic.Handlers.Sponsors.ResponoseDtos;
using MediatR;

namespace Alpaki.Logic.Handlers.Sponsors.GetSponsors
{
    public class GetSponsorsRequest : IRequest<GetSponsorsResponse>,IPagedRequest, IOrderedRequest
    {
        public GetSponsorsRequest()
        {
            OrderBy = nameof(SponsorItem.SponsorId);
            Asc = true;
            Page = null;
        }
        public SponsorCooperationEnum? CooperationType { get; set; }
        public string Search { get; set; }
        public int? Page { get; set; }
        public bool Asc { get; set; }
        public string OrderBy { get; set; }
    }
}