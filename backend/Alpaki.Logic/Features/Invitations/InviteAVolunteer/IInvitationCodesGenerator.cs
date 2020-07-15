namespace Alpaki.Logic.Features.Invitations.InviteAVolunteer
{
    public interface IInvitationCodesGenerator
    {
        string Generate(int length);
    }
}