namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteerResponse
    {
        public long UserId { get; }
        public string Token { get; }

        public RegisterVolunteerResponse(long userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}