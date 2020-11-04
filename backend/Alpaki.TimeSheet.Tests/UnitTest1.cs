using Alpaki.TimeSheet.Database;
using NSubstitute;
using Xunit;

namespace Alpaki.TimeSheet.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sss = Substitute.For<ITimeSheetDatabaseContext>();
            sss.Migrate();
            //sss.Received(0).Migrate();
            sss.DidNotReceiveWithAnyArgs().Migrate();
        }
    }
}
