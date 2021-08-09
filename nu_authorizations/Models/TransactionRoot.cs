using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace nu_authorizations.Models
{
    public class TransactionRoot
    {
        [JsonProperty("transaction")]
        public Transaction transaction { get; set; }
    }
}
