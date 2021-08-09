using Newtonsoft.Json;

namespace nu_authorizations.Models
{
    public class Account
    {
        [JsonProperty("active-card")]
        public bool? activeCard { get; set; }
        [JsonProperty("available-limit")]
        public int? availableLimit { get; set; }
    }
}
