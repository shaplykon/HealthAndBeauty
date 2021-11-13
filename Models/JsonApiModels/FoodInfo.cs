using Newtonsoft.Json;

namespace HealthAndBeauty.Models
{
    public class Foods
    {
        [JsonProperty("foods")]
        public NutritionInfo[] Info { get; set; }
    }

    public class NutritionInfo  
    {
        [JsonProperty("nf_calories")]
        public string Calorific { get; set; }
    }

    public class SearchResults
    {
        [JsonProperty("common")]
        public SearchResult[] SearchResult { get; set; }
    }
    public class SearchResult
    {
        [JsonProperty("food_name")]
        public string FoodName { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }

}
