using System;
using System.Collections.Generic;
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
                .SingleOrDefaultAsync(p => p.Year == request.Year && p.Month == request.Month && p.UserId == userId, cancellationToken);

            if (timePeriod == null)
            {
                timePeriod = new TimeSheetPeriod
                {
                    Year = request.Year,
                    Month = request.Month,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    TimeEntries = new List<TimeEntry>()
                };

                await _databaseContext.TimeSheetPeriods.AddAsync(timePeriod, cancellationToken);
            }

            timePeriod.TimeEntries = request.Entries.Where(e => e.Hours > 0).Select(e => new TimeEntry
            {
                Year = request.Year,
                Month = request.Month,
                Day = e.Day,
                Hours = e.Hours,
                UserId = userId
            }).ToList();

            await _databaseContext.SaveChangesAsync(cancellationToken);

            var response = new UpdateTimeSheetResponse();

            return response;
        }
    }
}
