using MediatR;

namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteer : IRequest<RegisterVolunteerResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Brand { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}