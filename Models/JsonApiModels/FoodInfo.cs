using Newtonsoft.Json;

namespace HealthAndBeauty.Models
{

    public class Foods
    {
        [JsonProperty("foods")]
        public Food[] info { get; set; }
    }

    public class Food
    {
        [JsonProperty("nf_calories")]
        public string Calorific { get; set; }

    }
}
