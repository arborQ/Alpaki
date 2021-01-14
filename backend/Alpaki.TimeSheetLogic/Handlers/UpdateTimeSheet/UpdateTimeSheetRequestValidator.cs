using System.Linq;
using Alpaki.TimeSheet.Database;
using FluentValidation;

namespace Alpaki.Logic.Handlers.UpdateTimeSheet
{
    public class UpdateTimeSheetRequestValidator : AbstractValidator<UpdateTimeSheetRequest>
    {

        public UpdateTimeSheetRequestValidator(ITimeSheetDatabaseContext databaseContext)
        {
            RuleFor(r => r.Year.Value).GreaterThanOrEqualTo(2020);
            RuleFor(r => r.Month.Value).GreaterThanOrEqualTo(1).LessThanOrEqualTo(12);
            RuleFor(r => r.Entries).Must(r => r.All(e => e.Hours >= 0 && e.Hours <= 24));
        }
    }
}
