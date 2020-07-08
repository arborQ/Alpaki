using Microsoft.EntityFrameworkCore;

namespace Alpaki.Perstine.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }
    }
}
