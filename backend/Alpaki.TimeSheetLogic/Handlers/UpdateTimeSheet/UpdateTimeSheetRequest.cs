using System.Collections.Generic;
using MediatR;

namespace Alpaki.Logic.Handlers.UpdateTimeSheet
{
    public class UpdateTimeSheetRequest : IRequest<UpdateTimeSheetResponse>
    {
        public UpdateTimeSheetRequest()
        {

        }

        public int Year { get; set; }

        public int Month { get; set; }

        public IReadOnlyCollection<TimeEntryRequest> Entries { get; set; }

        public class TimeEntryRequest
        {
            public int Day { get; set; }

            public decimal Hours { get; set; }
        }
    }
}
