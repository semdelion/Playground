using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Playground.Core.Model
{
    public class ContactResult
    {
        [JsonProperty("results")]
        public List<Contact> Contacts { get; set; }
    }
}
