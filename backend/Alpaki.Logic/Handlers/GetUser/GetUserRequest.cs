using MediatR;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public long UserId { get; set; }
    }
}
