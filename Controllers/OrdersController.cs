using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Hubs;
using HealthAndBeauty.Models;
using HealthAndBeauty.Services.UserConnections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        [Authorize(Roles = "admin, manager")]
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

        [Authorize(Roles = "admin, manager, courier")]
        public IActionResult CourierOrders(Guid courierId)
        {
            ViewBag.Orders = ordersRepository.GetOrdersByCourierId(courierId);           
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
                SendAsync("Send", "You have been assigned a new order");

            return RedirectToAction("Index", "Orders");
        }


        [HttpPost]
        [Route("Orders/TakeOrder/{orderId}")]
        public async Task<ActionResult> TakeOrder(string orderId)
        {
            var courierId = userManager.GetUserId(User);
            var order = ordersRepository.GetOrder(orderId);
            var user = await userManager.FindByIdAsync(order.UserId.ToString());
            var status = "Taken for delivery";

            order.CourierId = Guid.Parse(courierId);
            order.Status = status;
            ordersRepository.UpdateOrder(order);

            await notificationHub.Clients.Client(userConnectionManager.GetConnectionIdByName(user.UserName)).
                SendAsync("Send", "Your order was taken for delivery!");

            return Json(new { status = status});
        }
    }
}
