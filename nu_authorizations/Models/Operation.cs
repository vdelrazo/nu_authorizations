using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace nu_authorizations.Models
{
    public class Operation
    {
        [JsonProperty("account")]
        public Account account { get; set; }
        [JsonProperty("transaction")]
        public Transaction transaction { get; set; }
        [JsonProperty("violations")]
        public string[] violations { get; set; }
    }
}
