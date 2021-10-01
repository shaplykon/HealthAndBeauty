using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthAndBeauty.Controllers
{
    public class OrdersController : Controller
    {
        OrdersRepository ordersRepository;
        public OrdersController(OrdersRepository _ordersRepository)
        {
            ordersRepository = _ordersRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Orders = ordersRepository.GetOrders();
            return View();
        }
    }
}
