using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpServer.Library.IpConfig
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IpInfoData
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Location { get; set; }

        [JsonProperty("org")]
        public string Organization { get; set; }
    }
}
