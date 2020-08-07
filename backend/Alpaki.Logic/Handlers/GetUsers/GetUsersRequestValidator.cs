using Alpaki.CrossCutting.Requests;
using Alpaki.Logic.Validators;
using FluentValidation;

namespace Alpaki.Logic.Handlers.GetUsers
{
    public class GetUsersRequestValidator : AbstractValidator<GetUsersRequest>
    {
        public GetUsersRequestValidator(PagedRequestValidator pagedValidator)
        {
            RuleFor(r => r).SetValidator(pagedValidator);
        }
    }
}
