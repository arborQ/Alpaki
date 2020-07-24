namespace Alpaki.Logic.Handlers.ChangeUserRole
{
    public static class Exceptions
    {
        public class UserNotFoundException : LogicException
        {
            public override string Code => "user_not_found";

            public UserNotFoundException(long userId) : base($"Nie znaleziono użytkownika o identyfikatorze: '{userId}'.")
            {
            }
        }

        public class UserWithInvalidRoleException : LogicException
        {
            public override string Code => "user_with_invalid_role";

            public UserWithInvalidRoleException() : base($"Nie można zmienić roli użytkownika, który jest administratorem.")
            {
            }
        }
    }
}