using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.TimeSheet.Database;
using Alpaki.TimeSheet.Database.Models;
using MediatR;

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
            var entries = _databaseContext.TimeEntries.Where(e => e.Year == request.Year && e.Month == request.Month);
            _databaseContext.TimeEntries.RemoveRange(entries);

            _databaseContext.TimeEntries.AddRange(request.Entries.Where(e => e.Hours > 0).Select(e => new TimeEntry { Year = request.Year, Month = request.Month, Day = e.Day, Hours = e.Hours, UserId = 1 }));

            await _databaseContext.SaveChangesAsync();

            var response = new UpdateTimeSheetResponse();

            return response;
        }
    }
}
