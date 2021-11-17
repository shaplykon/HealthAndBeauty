using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Hubs;
using HealthAndBeauty.Models;
using HealthAndBeauty.Models.OrderModels;
using HealthAndBeauty.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace HealthAndBeauty.Controllers
{
    public class FoodSetsController : Controller
    {
        private OrdersRepository _ordersRepository;
        private FoodSetsRepository _foodSetsRepository;
        private UserManager<IdentityUser> _userManager;
        private GoogleMapsRepository _googleMapsRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IHubContext<NotificationHub> _notificationHub;
        private ILogger<FoodSetsController> _logger;

        public FoodSetsController(FoodSetsRepository foodSetsRepository,
            OrdersRepository ordersRepository,
            UserManager<IdentityUser> userManager,
            GoogleMapsRepository googleMapsRepository,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FoodSetsController> logger,
            IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
            _webHostEnvironment = webHostEnvironment;
            _ordersRepository = ordersRepository;
            _foodSetsRepository = foodSetsRepository;
            _userManager = userManager;
            _googleMapsRepository = googleMapsRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var ingredientsList = new List<Ingredient>();
            for (int i = 0; i < 5; i++)
            {
                ingredientsList.Add(new Ingredient());
            }
            ViewBag.FoodSet = new FoodSet { Ingredients = ingredientsList };

            return View();
        }

        [HttpPost]
        public IActionResult Edit(FoodSetViewModel foodSetViewModel)
        {
            if (ModelState.IsValid)
            {
                FoodSet foodSet = (FoodSet)foodSetViewModel;
                foodSet.ImageData = UploadedFile(foodSetViewModel);
                _foodSetsRepository.AddFoodSet(foodSet);
            }
            return RedirectToAction("Edit");
        }

        private string UploadedFile(FoodSetViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageData != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/foodSets");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageData.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageData.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        [HttpPost]
        public IActionResult CommentAdd(Comment comment, int foodSetId)
        {

            if (ModelState.IsValid)
            {
                comment.UserId = _userManager.GetUserId(HttpContext.User);
                comment.Date = DateTime.Now;
                _foodSetsRepository.AddComment(comment, foodSetId);
            }

            return RedirectToAction("Detail", new { Id = foodSetId });
        }

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            FoodSet foodSet;
            foodSet = _foodSetsRepository.GetFoodSetsById(Id);
            ViewBag.FoodSet = foodSet;

            if (User.Identity.IsAuthenticated)
            {
                if (_foodSetsRepository.IsInShoppingCart(Guid.Parse(_userManager.GetUserId(HttpContext.User)), Id))
                    ViewBag.isInCart = true;

                else
                    ViewBag.isInCart = false;
            }
            else
            {
                ViewBag.isInCart = false;
            }
            ViewBag.Comments = _foodSetsRepository.GetCommentBySetId(Id);
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult DeleteFoodSet(int Id)
        {
            _foodSetsRepository.DeleteFoodSetById(Id);
            _logger.LogWarning($"Food set with Id {Id} was deleted");
            return RedirectToAction("Index", "Home");
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult DeleteComment(int commentId, int foodSetId)
        {
            _foodSetsRepository.DeleteCommentById(commentId);
            _logger.LogWarning($"Comment with Id {commentId} was deleted from food set with Id {foodSetId}");
            return RedirectToAction("Detail", new { Id = foodSetId });
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult AddToShoppingCart(int foodSetId)
        {
            Guid userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            if (_foodSetsRepository.IsInShoppingCart(userId, foodSetId))
            {
                _foodSetsRepository.DeleteFromShoppingCart(userId, foodSetId);
            }
            else
            {
                _foodSetsRepository.AddToShoppingCart(
                new ShoppingCart
                {
                    Id = 0,
                    UserId = userId,
                    FoodSetId = foodSetId,
                    Date = DateTime.Now,

                });
            }
            return RedirectToAction("Detail", new { Id = foodSetId });
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            Guid userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            List<ShoppingCart> shoppingCarts = _foodSetsRepository.GetShoppingCartByUserId(userId).ToList();
            List<FoodSet> foodSets = new List<FoodSet>();
            double totalPrice = 0;

            foreach (ShoppingCart shoppingCart in shoppingCarts)
            {
                FoodSet set = _foodSetsRepository.GetFoodSetsById(shoppingCart.FoodSetId);
                totalPrice += 30;
                foodSets.Add(set);
            }

            var addressesList = new List<SelectListItem>();

            var addresses = _googleMapsRepository.GetCoordinates();

            foreach (Address address in addresses)
            {
                addressesList.Add(new SelectListItem
                {
                    Value = address.AddressName,
                    Text = address.AddressName
                });
            }

            var user = await _userManager.GetUserAsync(User);

            ViewBag.totalPrice = totalPrice;
            ViewBag.FoodSets = foodSets;
            ViewBag.Addresses = addressesList;
            ViewBag.Phone = await _userManager.GetPhoneNumberAsync(user);
            OrderViewModel orderViewModel = new OrderViewModel { IsCash = true, IsDelivery = true };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderViewModel orderViewModel, string addressName)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                List<int> foodSetsIds = _foodSetsRepository.GetShoppingCartByUserId(userId).Select(cart => cart.FoodSetId).ToList();

                var orderId = _ordersRepository.AddOrder(new Order
                {
                    Id = 0,
                    CourierId = Guid.Empty,
                    ReceiptDate = DateTime.Now,
                    Status = orderViewModel.IsDelivery ? "Wait for courier" : "Ready",
                    UserId = userId,
                    IsCash = orderViewModel.IsCash,
                    IsDelivery = orderViewModel.IsDelivery,
                    Address = orderViewModel.IsDelivery ? orderViewModel.Address : addressName,
                    PhoneNumber = orderViewModel.PhoneNumber
                });

                List<int> orderItemsIds = new List<int>();

                foreach (int id in foodSetsIds)
                {
                    orderItemsIds.Add(_ordersRepository.AddOrderItems(new OrderItem
                    {
                        Id = 0,
                        FoodSetId = id,
                        OrderId = orderId
                    }));
                }

                if (orderId != 0 && orderItemsIds.Count != 0)
                {
                    foreach (int foodSetsId in foodSetsIds)
                    {
                        _foodSetsRepository.DeleteFromShoppingCart(userId, foodSetsId);
                    }

                    _notificationHub.Clients.Group("Managers").SendAsync("Send", $"New order with ID {orderId} was added!");
                    // TODO: implement sending of order confirmation message
                }
            }

            return RedirectToAction("ShoppingCart");
        }
    }
}
