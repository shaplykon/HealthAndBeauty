using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models.JsonApiModels
{
    public class AddressInfo
    {
        [JsonProperty("results")]
        public List<AddressResult> AddressResults { get; set; }
    }
    public class AddressResult
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}
