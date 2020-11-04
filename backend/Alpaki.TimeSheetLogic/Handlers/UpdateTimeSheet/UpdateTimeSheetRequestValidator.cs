using Alpaki.TimeSheet.Database;
using FluentValidation;

namespace Alpaki.Logic.Handlers.UpdateTimeSheet
{
    public class UpdateTimeSheetRequestValidator : AbstractValidator<UpdateTimeSheetRequest>
    {

        public UpdateTimeSheetRequestValidator(ITimeSheetDatabaseContext databaseContext)
        {
            RuleFor(r => r.Year).GreaterThanOrEqualTo(2020);
            RuleFor(r => r.Month).GreaterThanOrEqualTo(1).LessThanOrEqualTo(12);
        }
    }
}
