using MediatR;

namespace Alpaki.Logic.Handlers.DeleteUser
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        public long UserId { get; set; }
    }
}
