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
using NLog;
using HealthAndBeauty.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using HealthAndBeauty.Services.UserConnections;

namespace HealthAndBeauty.Controllers
{
    public class HomeController : Controller
    {
        private FoodSetsRepository _foodSetsRepository;
        ILogger<HomeController> _logger;
        IHubContext<NotificationHub> _notificationHub;
        private readonly IUserConnectionManager _userConnectionManager;
        public HomeController( 
            FoodSetsRepository foodSetsRepository, 
            ILogger<HomeController> logger, 
            IHubContext<NotificationHub> notificationHub,
              IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
            _foodSetsRepository = foodSetsRepository;
            _logger = logger;
            _notificationHub = notificationHub;
        }

        public ActionResult Index()
        {
            ViewBag.FoodSets = _foodSetsRepository.GetActiveFoodSetsList();
            return View();
        }

        public IActionResult Privacy()
        {
            string connectionId = _userConnectionManager.GetConnectionIdByName("admin@admin.admin");
            _notificationHub.Clients.Client(connectionId).SendAsync("Send", "");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
