using System.Text.Json.Serialization;

namespace Contact_Management_System.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
