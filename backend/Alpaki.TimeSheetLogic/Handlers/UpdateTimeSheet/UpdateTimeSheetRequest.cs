using System.Collections.Generic;
using Alpaki.CrossCutting.ValueObjects;
using MediatR;

namespace Alpaki.Logic.Handlers.UpdateTimeSheet
{
    public class UpdateTimeSheetRequest : IRequest<UpdateTimeSheetResponse>
    {
        public UpdateTimeSheetRequest()
        {

        }

        public Year Year { get; set; }

        public Month Month { get; set; }

        public IReadOnlyCollection<TimeEntryRequest> Entries { get; set; }

        public class TimeEntryRequest
        {
            public int Day { get; set; }

            public decimal Hours { get; set; }
        }
    }
}
