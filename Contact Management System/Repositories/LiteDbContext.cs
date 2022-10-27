using LiteDB;
using Microsoft.Extensions.Options;

namespace Contact_Management_System.Repositories
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DatabaseLocation);
        }

        // use this constructor for testing with in memory db.
        public LiteDbContext()
        {
            Database = new LiteDatabase(new MemoryStream());
        }
    }
}
