using Alpaki.Moto.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.MotoLogic.Handlers.UpdateBrand
{
    public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
    {
        public UpdateBrandRequestValidator(IMotoDatabaseContext motoDatabaseContext)
        {
            RuleFor(b => b.BrandId).MustAsync((brandId, c) => motoDatabaseContext.Brands.AnyAsync(b => b.BrandId == brandId, c));
        }
    }
}
