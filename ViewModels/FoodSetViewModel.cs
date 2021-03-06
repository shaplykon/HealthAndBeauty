using HealthAndBeauty.Models;
using Microsoft.AspNetCore.Http;
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

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Image")]
        public IFormFile ImageData { get; set; }

        [Required(ErrorMessage = "Please input price")]
        [Display(Name = "Price")]
        public int Price{ get; set; }

        [Required(ErrorMessage = "Please input delivery time")]
        [Display(Name = "Time")]
        public int DeliveryTime { get; set; }

        [Required(ErrorMessage = "Please input calorific")]
        [Display(Name = "Calorific")]
        public int Calorific
        {
            get; set;
        }
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
            if (viewModel.Price != default)
            {
                foodSet.Price = viewModel.Price;
            }
            if (viewModel.DeliveryTime != default)
            {
                foodSet.DeliveryTime = viewModel.DeliveryTime;
            }
            if (viewModel.Calorific != default)
            {
                foodSet.Calorific = viewModel.Calorific;
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
            if (viewModel.ImageData != null)
            {
                foodSet.ImageData = viewModel.ImageData.FileName;
            }

            return foodSet;
        }
    }
}
