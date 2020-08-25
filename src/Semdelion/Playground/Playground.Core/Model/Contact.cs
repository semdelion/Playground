using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playground.Core.Model
{
    public class Contact
    {
        [JsonProperty("picture")]
        public Photo Photo { get; set; }
        public Name Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}