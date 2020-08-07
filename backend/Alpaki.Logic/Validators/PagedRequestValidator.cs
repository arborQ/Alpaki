using System;
using System.Collections.Generic;
using System.Text;
using Alpaki.CrossCutting.Requests;
using FluentValidation;

namespace Alpaki.Logic.Validators
{
    public class PagedRequestValidator : AbstractValidator<IPagedRequest>
    {
        public PagedRequestValidator()
        {
            RuleFor(r => r.Page).GreaterThan(0).When(r => r.Page.HasValue).WithMessage("Numer strony nie może być mniejszy niż 1");
        }
    }
}
