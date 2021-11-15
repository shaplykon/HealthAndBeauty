using Flurl.Http;
using HealthAndBeauty.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty
{
    public static class NutritionixApiService
    {
        private static string _calorificBaseUrl;
        private static string _searchBaseUrl;
        private static string _appId;
        private static string _appKey;
        private static string _userId;

        static NutritionixApiService()
        {
            _calorificBaseUrl = "https://trackapi.nutritionix.com/v2/natural/nutrients";
            _searchBaseUrl = "https://trackapi.nutritionix.com/v2/search/instant";
   
            _appId = "3b7dd245";
            _appKey = "5ac10907fe0550c10d56c9a7db9612c5";
            _userId = "1";
        }

        public static async Task<NutritionInfo> GetProductCalorific(string product)
        {
            Foods foodsArray = new Foods();
            if (!string.IsNullOrEmpty(product))
            {
                try
                {
                    foodsArray = await _calorificBaseUrl
                        .WithHeader("x-app-id", _appId)
                        .WithHeader("x-app-key", _appKey)
                        .WithHeader("x-remote-user-id", _userId)
                        .PostJsonAsync(new { query = product }).ReceiveJson<Foods>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return foodsArray.Info?[0];
        }

        public static async Task<SearchResults> GetAutocompleteProductSuggestions(string product)
        {
            SearchResults foodsArray = null;
            try
            {
                foodsArray = await _searchBaseUrl
                    .WithHeader("x-app-id", _appId)
                    .WithHeader("x-app-key", _appKey)
                    .WithHeader("x-remote-user-id", _userId)
                    .PostJsonAsync(new { query = product }).ReceiveJson<SearchResults>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return foodsArray;
        }
    }
}
