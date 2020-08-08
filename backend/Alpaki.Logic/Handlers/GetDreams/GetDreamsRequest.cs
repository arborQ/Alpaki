using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Requests;
using MediatR;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamsRequest : IRequest<GetDreamsResponse>, IPagedRequest, IOrderedRequest
    {
        public GetDreamsRequest()
        {
            Categories = new long[0];
            OrderBy = "DreamId";
        }

        public string SearchName { get; set; }

        public GenderEnum? Gender { get; set; }

        public DreamStateEnum? Status { get; set; }

        public int? AgeFrom { get; set; }

        public int? AgeTo { get; set; }

        public long[] Categories { get; set; }

        public bool Asc { get; set; }

        public string OrderBy { get; set; }

        public int? Page { get; set; }
    }
}
