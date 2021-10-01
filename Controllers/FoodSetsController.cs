﻿using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models;
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
        private FoodSetsRepository _foodSetsRepository;
        UserManager<IdentityUser> userManager;
        public FoodSetsController(FoodSetsRepository foodSetsRepository, UserManager<IdentityUser> _userManager)
        {
            _foodSetsRepository = foodSetsRepository;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var ingredientsList = new List<Ingredient>();
            for(int i = 0; i < 5; i++)
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

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            FoodSet sneakers;
            sneakers = _foodSetsRepository.GetFoodSetsById(Id);
            ViewBag.FoodSet = sneakers;

            if (User.Identity.IsAuthenticated)
            {
                if (_foodSetsRepository.IsInShoppingCart(Guid.Parse(userManager.GetUserId(HttpContext.User)), Id))
                {
                    ViewBag.isInCart = true;
                }

                else
                {
                    ViewBag.isInCart = false;
                }
            }
            else
            {
                ViewBag.isInCart = false;
            }

            return View();
        }

       /* [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public IActionResult AddToShoppingCart(int foodSetId)
        {
            Guid userId = Guid.Parse(userManager.GetUserId(HttpContext.User));
            if (purchaseRepository.IsInPurchases(userId, sneakersId))
            {
                purchaseRepository.DeletePurchaseById(userId, sneakersId);
            }
            else
            {
                purchaseRepository.SavePurchase(
                new Purchase
                {
                    PurchaseId = 0,
                    UserId = userId,
                    SneakersId = sneakersId,
                    Date = DateTime.Now,

                });
            }
            return RedirectToAction("SneakersDetail", "Sneakers", new { Id = sneakersId });
        }*/

    }
}
