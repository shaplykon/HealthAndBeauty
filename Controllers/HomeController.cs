using HealthAndBeauty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Flurl.Http;
using Flurl;
using HealthAndBeauty.Data.Repositories;

namespace HealthAndBeauty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FoodSetsRepository _foodSetsRepository;
            

        public HomeController(ILogger<HomeController> logger, FoodSetsRepository foodSetsRepository)
        {
            _logger = logger;
            _foodSetsRepository = foodSetsRepository;
        }

        public async Task<ActionResult> Index()
        {

            string WEBSERVICE_URL = "https://trackapi.nutritionix.com/v2/natural/nutrients";
            Foods foodsArray = null;
            string product = "egg";
            try
            {
                foodsArray = await WEBSERVICE_URL
                    .WithHeader("x-app-id", "3b7dd245")
                    .WithHeader("x-app-key", "5ac10907fe0550c10d56c9a7db9612c5")
                    .WithHeader("x-remote-user-id", "1")
                    .PostJsonAsync(new { query = product }).ReceiveJson<Foods>();
            }

            /* FoodApi.GetProductCalirific("egg"); */ 

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            ViewBag.Response = string.Format($"{product} calorific: {foodsArray.info[0].Calorific}");
            ViewBag.FoodSets = _foodSetsRepository.GetFoodSets();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
