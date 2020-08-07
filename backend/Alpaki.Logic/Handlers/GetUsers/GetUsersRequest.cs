using Alpaki.CrossCutting.Requests;
using MediatR;
using static Alpaki.Logic.Handlers.GetUsers.GetUsersResponse;

namespace Alpaki.Logic.Handlers.GetUsers
{
    public class GetUsersRequest : IRequest<GetUsersResponse>, IPagedRequest, IOrderedRequest
    {
        public GetUsersRequest()
        {
            OrderBy = nameof(UserListItem.UserId);
            Asc = true;
        }

        public string Search { get; set; }

        public int? Page { get; set; }

        public bool Asc { get; set; }

        public string OrderBy { get; set; }
    }
}
