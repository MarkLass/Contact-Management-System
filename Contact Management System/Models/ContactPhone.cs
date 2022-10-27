using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Management_System.Models
{
    public enum PhoneType
    {
        home,
        work,
        mobile
    }

    public class ContactPhone
    {
        public string number { get; set; }
        public PhoneType type { get; set; }
    }
}
