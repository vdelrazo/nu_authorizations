using Newtonsoft.Json;
using System;

namespace nu_authorizations.Models
{
    public class Transaction
    {
        [JsonProperty("merchant")]
        public string merchant;
        [JsonProperty("amount")]
        public int amount;
        [JsonProperty("time")]
        public DateTime time;
    }
}
