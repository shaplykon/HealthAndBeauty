using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Hubs;
using HealthAndBeauty.Models;
using HealthAndBeauty.Services.UserConnections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace HealthAndBeauty.Controllers
{
    public class OrdersController : Controller
    {
        OrdersRepository ordersRepository;
        UserManager<IdentityUser> userManager;
        IHubContext<NotificationHub> notificationHub;
        readonly IUserConnectionManager userConnectionManager;

        public OrdersController(
                    OrdersRepository _ordersRepository,
                    UserManager<IdentityUser> _userManager,
                    IHubContext<NotificationHub> _notificationHub,
                    IUserConnectionManager _userConnectionManager)
        {
            userConnectionManager = _userConnectionManager;
            notificationHub = _notificationHub;
            userManager = _userManager;
            ordersRepository = _ordersRepository;
        }
        public IActionResult Index()
        {
            var couriersList = new List<SelectListItem>();

            ViewBag.Orders = ordersRepository.GetOrders();

            foreach (IdentityUser user in userManager.GetUsersInRoleAsync("courier").Result)
            {
                couriersList.Add(new SelectListItem { Text = user.UserName, Value = user.Id, Selected = false });
            }

            ViewBag.Couriers = couriersList;
            return View();
        }

        [HttpPost]
        [Route("Orders/BindCourier/{orderId}/{courierId}")]
        public async Task<ActionResult> BindCourier(string orderId, string courierId)
        {
            var order = ordersRepository.GetOrder(orderId);
            var courier = await userManager.FindByIdAsync(courierId);

            order.CourierId = Guid.Parse(courierId);
            order.Status = "Wait for delivery";
            ordersRepository.UpdateOrder(order);

            await notificationHub.Clients.Client(userConnectionManager.GetConnectionIdByName(courier.UserName)).
                SendAsync("Send", "You have been assigned a new client");

            return RedirectToAction("Index");
        }
    }
}
