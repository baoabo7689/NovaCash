using Newtonsoft.Json;

namespace NovaCash.Sportsbook.Clients.Requests
{
    public class BetDetailRequest
    {
        [JsonProperty(PropertyName = "vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty(PropertyName = "version_key")]
        public string VersionKey { get; set; }

        [JsonProperty(PropertyName = "options")]
        public string Options { get; set; }
    }
}