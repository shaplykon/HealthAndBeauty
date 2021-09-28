using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models;
using HealthAndBeauty.ViewModels;
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
        public FoodSetsController(FoodSetsRepository foodSetsRepository)
        {
            _foodSetsRepository = foodSetsRepository;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            ViewBag.FoodSet = new FoodSet { Ingredients = new List<Ingredient>{ new Ingredient(), new Ingredient() }};

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


    }
}
