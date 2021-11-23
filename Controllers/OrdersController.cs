using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Helpers;
using HealthAndBeauty.Hubs;
using HealthAndBeauty.Models;
using HealthAndBeauty.Models.OrderModels;
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
        HistoryRepository historyRepository;
        UserManager<IdentityUser> userManager;
        IHubContext<NotificationHub> notificationHub;
        readonly IUserConnectionManager userConnectionManager;

        public OrdersController(
                    HistoryRepository _historyRepository,
                    OrdersRepository _ordersRepository,
                    UserManager<IdentityUser> _userManager,
                    IHubContext<NotificationHub> _notificationHub,
                    IUserConnectionManager _userConnectionManager)
        {
            historyRepository = _historyRepository;
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
            order.Status = Constants.WAIT_FOR_COUTIER_STATUS;
            ordersRepository.UpdateOrder(order);

            await notificationHub.Clients.Client(userConnectionManager.GetConnectionIdByName(courier.UserName)).
                SendAsync("Send", "You have been assigned a new order");

            return RedirectToAction("Index", "Orders");
        }


        [HttpPost]
        [Authorize(Roles = "courier")]
        [Route("Orders/TakeOrder/{orderId}")]
        public async Task<ActionResult> TakeOrder(string orderId)
        {
            var courierId = userManager.GetUserId(User);
            var order = ordersRepository.GetOrder(orderId);

            if (order.Status == Constants.WAIT_FOR_COUTIER_STATUS)
            {
                var user = await userManager.FindByIdAsync(order.UserId.ToString());

                order.CourierId = Guid.Parse(courierId);
                order.Status = Constants.IN_DELIVERY_STATUS;
                ordersRepository.UpdateOrder(order);

                historyRepository.AddHistory(new History
                {
                    CurrentStatus = Constants.IN_DELIVERY_STATUS,
                    UserId = Guid.Parse(user.Id),
                    OrderId = order.Id
                });

                //await notificationHub.Clients.Client(userConnectionManager.GetConnectionIdByName(user.UserName)).
                //    SendAsync("Send", "Your order was taken for delivery!").ConfigureAwait(false);
                return Json(new { status = Constants.IN_DELIVERY_STATUS });

            }
            else
            {
                return Json(new { status = Constants.WAIT_FOR_COUTIER_STATUS });
            }
           
        }

        [HttpPost]
        [Authorize(Roles = "courier")]
        [Route("Orders/DeliverOrder/{orderId}")]
        public async Task<ActionResult> DeliverOrder(string orderId)
        {
            var order = ordersRepository.GetOrder(orderId);

            if (order.Status == Constants.IN_DELIVERY_STATUS && userManager.GetUserId(User).Equals(order.CourierId.ToString()))
            {
                var user = await userManager.FindByIdAsync(order.UserId.ToString());
                order.Status = Constants.DELIVERED_STATUS;
                ordersRepository.UpdateOrder(order);

                historyRepository.AddHistory(
                new History
                {
                    CurrentStatus = Constants.DELIVERED_STATUS,
                    UserId = Guid.Parse(user.Id),
                    OrderId = order.Id
                });
                return Json(new { status = Constants.DELIVERED_STATUS});
            }

            return Json(new { status = Constants.IN_DELIVERY_STATUS });
        }

    }
}
