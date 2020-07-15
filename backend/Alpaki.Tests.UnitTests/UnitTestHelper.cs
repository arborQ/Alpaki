using System;
using System.Threading.Tasks;

namespace Alpaki.Tests.UnitTests
{
    public static class  UnitTestHelper
    {
        public static async Task SuppressException(Func<Task> action)
        {
            try
            {
                if (action != null) 
                    await action();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}