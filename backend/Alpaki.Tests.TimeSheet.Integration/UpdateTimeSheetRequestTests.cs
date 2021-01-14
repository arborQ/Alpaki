using System;
using System.Linq;
using System.Threading.Tasks;
using Alpaki.CrossCutting.ValueObjects;
using Alpaki.Logic.Handlers.UpdateTimeSheet;
using Alpaki.Tests.TimeSheet.Integration.Fixtures;
using MediatR;
using Xunit;
using static Alpaki.Logic.Handlers.UpdateTimeSheet.UpdateTimeSheetRequest;

namespace Alpaki.Tests.TimeSheet.Integration
{
    public class UpdateTimeSheetRequestTests : IntegrationTestsClass
    {
        private IMediator _sut;

        public UpdateTimeSheetRequestTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _sut = integrationTestsFixture.Mediator;
        }

        [Fact]
        public async Task Handle_CreateEntry()
        {
            // Arrange
            var request = new UpdateTimeSheetRequest { Year = Year.From(2001), Month = Month.From(1), Entries = new[] { new TimeEntryRequest { Day = 1, Hours = 10 } } };
            await _sut.Send(request, default);

            var any = IntegrationTestsFixture.DatabaseContext.TimeEntries.Where(t => t.Year == request.Year).Any();
            var count = IntegrationTestsFixture.DatabaseContext.TimeEntries.Count();

            Assert.True(any);
            Assert.Equal(1, count);
        }
    }
}
