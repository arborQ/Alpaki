using MediatR;

namespace Alpaki.Logic.Handlers.UpdateUserData
{
    public class UpdateUserDataRequest : IRequest<UpdateUserDataResponse>
    {
        public long UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Brand { get; set; }

        public string PhoneNumber { get; set; }
    }
}
