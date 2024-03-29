using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.ValueObjects;
using Alpaki.TimeSheet.Database;
using Alpaki.TimeSheet.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.UpdateTimeSheet
{
    public class UpdateTimeSheetHandler : IRequestHandler<UpdateTimeSheetRequest, UpdateTimeSheetResponse>
    {
        private readonly ITimeSheetDatabaseContext _databaseContext;

        public UpdateTimeSheetHandler(ITimeSheetDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UpdateTimeSheetResponse> Handle(UpdateTimeSheetRequest request, CancellationToken cancellationToken)
        {
            var userId = new UserId(1);

            var timePeriod = await _databaseContext.TimeSheetPeriods
                .Include(p => p.TimeEntries)
                .Include(p => p.DomainEvents)
                .SingleOrDefaultAsync(p => p.Year == request.Year && p.Month == request.Month && p.UserId == userId, cancellationToken);

            var timeEntries = request
                .Entries
                .Where(e => e.Hours > 0)
                .Select(e => new TimeEntry
                {
                    Year = request.Year,
                    Month = request.Month,
                    Day = e.Day,
                    Hours = e.Hours,
                    UserId = userId
                })
                .ToList();

            if (timePeriod == null)
            {
                timePeriod = new TimeSheetPeriod(request.Year, request.Month, userId, timeEntries);
                await _databaseContext.TimeSheetPeriods.AddAsync(timePeriod, cancellationToken);
            }

            timePeriod.UpdateHours(timeEntries);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            return new UpdateTimeSheetResponse();
        }
    }
}
