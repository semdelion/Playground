using Newtonsoft.Json;
using System.Collections.Generic;

namespace Semdelion.API.Models
{
    public class ContactResult
    {
        [JsonProperty("results")]
        public List<Contact> Contacts { get; set; }
    }

    public class Contact
    {
        [JsonProperty("picture")]
        public Photo Photo { get; set; }
        public Name Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Photo
    {
        public string Large { get; set; }
        public string Thumbnail { get; set; }
    }
}
