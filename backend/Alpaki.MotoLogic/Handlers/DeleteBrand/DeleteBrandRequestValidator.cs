using Alpaki.Moto.Database;
using FluentValidation;

namespace Alpaki.Logic.Handlers.DeleteBrand
{
    public class DeleteBrandRequestValidator : AbstractValidator<DeleteBrandRequest>
    {

        public DeleteBrandRequestValidator(IMotoDatabaseContext databaseContext)
        {
        }
    }
}
