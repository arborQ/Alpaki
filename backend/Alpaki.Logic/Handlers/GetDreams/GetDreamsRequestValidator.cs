using Alpaki.Logic.Validators;
using FluentValidation;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamsRequestValidator : AbstractValidator<GetDreamsRequest>
    {
        public GetDreamsRequestValidator(PagedRequestValidator pagedValidator)
        {
            RuleFor(r => r).SetValidator(pagedValidator);
        }
    }
}
