﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class CalorificCalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAutocompleteSuggestion(string product)
        {
            var q = new { label = "loh", value = "loh" };
 
            var suggestionsList = await NutritionixApiService.GetAutocompleteProductSuggestions(product);

            var query = from suggestion in suggestionsList.SearchResult
                        select new
                        {
                            label = suggestion.FoodName,
                            value = suggestion.FoodName
                        };
            return Json(query.Take(5));
        }
    }
}
