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

        [JsonProperty("nf_total_carbohydrate")]
        public string Carbohydrate { get; set; }

        [JsonProperty("nf_protein")]
        public string Protein { get; set; }

        [JsonProperty("nf_total_fat")]
        public string TotalFat { get; set; }

        [JsonProperty("nf_saturated_fat")]
        public string SaturatedFat { get; set; }

        [JsonProperty("nf_cholesterol")]
        public string Cholesterol { get; set; }

        [JsonProperty("nf_sodium")]
        public string Sodium { get; set; }

        [JsonProperty("nf_potassium")]
        public string Potassium { get; set; }

        [JsonProperty("serving_unit")]
        public string Unit { get; set; }

        [JsonProperty("serving_weight_grams")]
        public string Weight { get; set; }


        [JsonProperty("photo")]
        public ProductPhoto Photo { get; set; }

    }

    public class ProductPhoto
    {
        [JsonProperty("thumb")]
        public string PhotoUrl { get; set; }
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
