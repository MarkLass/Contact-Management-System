namespace Contact_Management_System.Repositories
{
    public class LiteDbOptions
    {
        public LiteDbOptions()
        {
            DatabaseLocation = "ContactDb.db";
        }
        public string DatabaseLocation { get; set; }
    }
}
