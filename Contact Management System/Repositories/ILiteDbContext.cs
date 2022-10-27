using LiteDB;

namespace Contact_Management_System.Repositories
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}
