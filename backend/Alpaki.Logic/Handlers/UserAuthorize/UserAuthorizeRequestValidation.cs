using FluentValidation;

namespace Alpaki.Logic.Services
{
    public class UserAuthorizeRequestValidation : AbstractValidator<UserAuthorizeRequest>, IValidator<UserAuthorizeRequest>
    {
        public UserAuthorizeRequestValidation()
        {
            RuleFor(d => d.Login).NotEmpty().WithMessage("Login jest wymagany");
            RuleFor(d => d.Password).NotEmpty().WithMessage("Hasło jest wymagane");
        }
    }
}
