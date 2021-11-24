using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class HistoryController : Controller
    {
        HistoryRepository historyRepository;
        UserManager<IdentityUser> userManager;
        public HistoryController(
            HistoryRepository _historyRepository,
            UserManager<IdentityUser> _userManager)
        {
            userManager = _userManager;
            historyRepository = _historyRepository;
        }
        public IActionResult Index()
        {
            var ordersHistory = historyRepository.GetHistoryByUserId(Guid.Parse(userManager.GetUserId(User))).AsEnumerable().
            GroupBy(history => history.OrderId).
            Select(group => new HistoryGroup{
                Key = group.Key, 
                History = group.OrderBy(group => group.CreateDate).ToList(),
                Count = group.Count()}).
            ToList();;

            ViewBag.ActiveOrdersHistory = ordersHistory.Where(order => order.History.Count < 4);
            ViewBag.OrdersHistory = ordersHistory.Where(order => order.History.Count == 4);
            return View();
        }
    }
}
