using Contact_Management_System.Models;

namespace Contact_Management_System.Repositories
{
    public interface IContactRepository
    {
        void Create(Contact? contact);
        Contact? GetById(int id);
        List<Contact> GetAll();
        void Update(Contact contact);
        void Delete(int id);
    }
}
