using Alpaki.Database;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateUserData
{
    public class UpdateUserDataRequestValidator : AbstractValidator<UpdateUserDataRequest>
    {
        public UpdateUserDataRequestValidator(IUserScopedDatabaseReadContext userScopedDatabaseReadContext, IDatabaseContext databaseContext)
        {
            RuleFor(r => r.UserId)
                .MustAsync((userId, cancellationToken) => userScopedDatabaseReadContext.Users.AnyAsync(u => u.UserId == userId, cancellationToken))
                .WithMessage(r => $"Użytkownik UserId=[{r.UserId}] nie istnieje");
            RuleFor(u => u.Email).NotEmpty().EmailAddress(EmailValidationMode.AspNetCoreCompatible).When(u => u.Email != null).WithMessage("Niepoprawny adres email.");
            RuleFor(u => u.FirstName).MaximumLength(250).NotEmpty().When(u => u.FirstName != null).WithMessage("Imię nie może być dłuższe niż 250 znaków");
            RuleFor(u => u.LastName).MaximumLength(250).NotEmpty().When(u => u.LastName != null).WithMessage("Nazwisko nie może być dłuższe niż 250 znaków");
            RuleFor(u => u.Brand).NotEmpty().When(u => u.Brand != null).WithMessage("Oddział nie może być pusty");
            RuleFor(u => u.PhoneNumber).NotEmpty().When(u => u.PhoneNumber != null).WithMessage("Numer telefonu nie może być pusty");

            RuleFor(u => u.ProfileImageId)
                .MustAsync((id, cancellationToken) => databaseContext.Images.AnyAsync(i => i.ImageId == id, cancellationToken))
                .When(u => u.ProfileImageId.HasValue)
                .WithMessage(a => $"Plik o Id=[{a.ProfileImageId}] nie istnieje");

            RuleFor(u => u.ProfileImageId)
                .MustAsync((id, cancellationToken) => databaseContext.Users.AllAsync(u => u.ProfileImageId != id, cancellationToken))
                .When(u => u.ProfileImageId.HasValue)
                .WithMessage(a => $"Plik o Id=[{a.ProfileImageId}] jest już przypisany do innego konta");
        }
    }
}
