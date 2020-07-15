namespace Alpaki.Logic.Features.Invitations
{
    public static class Exceptions
    {
        public class InvitationNotFoundException : LogicException
        {
            public override string Code => "invitation_not_found";

            public InvitationNotFoundException() : base("Brak zaproszenia dla podanego emaila.")
            {
            }
        }
        public class InvitationHasExpiredException : LogicException
        {
            public override string Code => "invitation_has_expired";

            public InvitationHasExpiredException() : base("Zaproszenie wygasło.")
            {
            }
        }
        public class InvitationHasBeenRevokedException : LogicException
        {
            public override string Code => "invitation_is_revoked";

            public InvitationHasBeenRevokedException() : base("Zaproszenie zostało unieważnione. Podano zbyt wiele razy nie poprawny kod.")
            {
            }
        }
        public class InvalidInvitationCodeException : LogicException
        {
            public override string Code => "invalid_invitation_code";

            public InvalidInvitationCodeException() : base("Podano nie poprawny kod zaproszenia.")
            {
            }
        }
        public class VolunteerAlreadyExistsException : LogicException
        {
            public override string Code => "volunteer_already_exits";

            public VolunteerAlreadyExistsException() : base("Wolontariusz o podanym emailu już istnieje.")
            {
            }
        }
    }
}