﻿using HealthAndBeauty.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.ViewModels
{
    public class FoodSetViewModel
    {
        public int? Id { get; set; } 
        [Required(ErrorMessage = "Please input latitude")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please input longtitutde")]
        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public static explicit operator FoodSet(FoodSetViewModel viewModel)
        {
            FoodSet foodSet = new FoodSet();

            if (viewModel.Name != default)
            {
                foodSet.Name = viewModel.Name;
            }
            if (viewModel.Description != default)
            {
                foodSet.Description = viewModel.Description;
            }
            if (viewModel.Ingredients != default)
            {
                foodSet.Ingredients = new List<Ingredient>();
                foreach(Ingredient ingredient in viewModel.Ingredients)
                {
                    if(ingredient != null)
                        foodSet.Ingredients.Add(ingredient);
                }
            }

            return foodSet;
        }
    }
}