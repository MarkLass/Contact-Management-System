using Contact_Management_System.Models;

namespace Contact_Management_System.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contacts = new();
        public void Create(Contact? contact)
        {
            if (contact is null)
                return;

            _contacts[contact.id] = contact;
        }

        public Contact? GetById(int id)
        {
            return _contacts.GetValueOrDefault(id);
        }

        public List<Contact> GetAll ()
        {
            return _contacts.Values.ToList();
        }

        public void Update(Contact contact)
        {
            _contacts[contact.id] = contact;
        }

        public void Delete (int id)
        { 
            _contacts.Remove(id);
        }
    }
}
