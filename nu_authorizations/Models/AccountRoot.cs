using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace nu_authorizations.Models
{
    public class AccountRoot
    {
        [JsonProperty("account")]
        public Account account { get; set; }
        [JsonProperty("violations")]
        public string[] violations { get; set; }
    }
}
