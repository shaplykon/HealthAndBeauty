using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models;
using HealthAndBeauty.Models.OrderModels;
using HealthAndBeauty.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class FoodSetsController : Controller
    {
        private OrdersRepository _ordersRepository;
        private FoodSetsRepository _foodSetsRepository;
        UserManager<IdentityUser> userManager;
        public FoodSetsController(FoodSetsRepository foodSetsRepository,
            OrdersRepository ordersRepository,
            UserManager<IdentityUser> _userManager)
        {
            _ordersRepository = ordersRepository;
            _foodSetsRepository = foodSetsRepository;
            userManager = _userManager;
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
                foodSet.Ingredients = foodSetViewModel.Ingredients;
                _foodSetsRepository.AddFoodSet(foodSet);
            }
            return RedirectToAction("Edit");
        }
        [HttpPost]
        public IActionResult CommentAdd(Comment comment, int foodSetId)
        {

            if (ModelState.IsValid)
            {
                comment.UserId = userManager.GetUserId(HttpContext.User);
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
                if (_foodSetsRepository.IsInShoppingCart(Guid.Parse(userManager.GetUserId(HttpContext.User)), Id))
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

        [HttpPost]
        public IActionResult DeleteComment(int commentId, int foodSetId)
        {
            _foodSetsRepository.DeleteCommentById(commentId);
            //logger.LogWarning("Comment with Id {0} was deleted", commentId);
            return RedirectToAction("Detail", new { Id = foodSetId });
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult AddToShoppingCart(int foodSetId)
        {
            Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
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
        public IActionResult ShoppingCart()
        {
            Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
            List<ShoppingCart> shoppingCarts = _foodSetsRepository.GetShoppingCartByUserId(userId).ToList();
            List<FoodSet> foodSets = new List<FoodSet>();
            double totalPrice = 0;
            foreach (ShoppingCart shoppingCart in shoppingCarts)
            {
                FoodSet set = _foodSetsRepository.GetFoodSetsById(shoppingCart.FoodSetId);
                totalPrice += 30;
                foodSets.Add(set);
            }
            ViewBag.totalPrice = totalPrice;
            ViewBag.FoodSets = foodSets;

            OrderViewModel orderViewModel = new OrderViewModel { IsCash = true, IsDelivery = true };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
                List<int> foodSetsIds = _foodSetsRepository.GetShoppingCartByUserId(userId).Select(cart => cart.FoodSetId).ToList();
                    
                var orderId = _ordersRepository.AddOrder(new Order
                {
                    Id = 0,
                    CourierId = Guid.Empty,
                    ReceiptDate = DateTime.Now,
                    Status = orderViewModel.IsDelivery ? "Wait for courier" : "Ready",
                    UserId = userId,
                    IsCash = orderViewModel.IsCash,
                    IsDelivery = orderViewModel.IsDelivery
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

                    // TODO: implement sending of order confirmation message
                }
            }
           
            return RedirectToAction("ShoppingCart");
        }
    }
}
