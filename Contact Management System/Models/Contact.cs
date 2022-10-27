using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Management_System.Models
{
    public class Contact
    {
        public int id { get; set; }
        public ContactName name { get; set; }
        public ContactAddress address { get; set; }
        public List<ContactPhone> phone { get; set; }
        public string email { get; set; }

    }
}
