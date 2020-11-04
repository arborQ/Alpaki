using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.TimeSheet.Database;
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
            _databaseContext.TimeEntries.RemoveRange()

            var response = new UpdateTimeSheetResponse();

            return response;
        }
    }
}
