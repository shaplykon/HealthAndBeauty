using HealthAndBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAndBeauty.Data.Repositories
{
    public class FoodSetsRepository
    {
        private ApplicationDbContext context;

        public FoodSetsRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public List<FoodSet> GetFoodSetsList()
        {
            List<FoodSet> foodSets = context.FoodSets.ToList();

            foreach(FoodSet foodSet in foodSets)
            {
                foodSet.Ingredients = context.Ingredients.Where(ingredient => ingredient.FoodSet.Id == foodSet.Id).ToList();
            }
            return foodSets;
        }

        public void AddFoodSet(FoodSet foodSet)
        {
            context.FoodSets.Add(foodSet);
            context.SaveChanges();
        }

        internal FoodSet GetFoodSetsById(int id)
        {
            var foodSet = context.FoodSets.Where(set => set.Id == id).FirstOrDefault();
            foodSet.Ingredients = context.Ingredients.Where(ingredient => ingredient.FoodSet.Id == id).ToList();
            return foodSet;
        }

        internal bool IsInShoppingCart(Guid guid, int id)
        {
            IQueryable<ShoppingCart> shoppingCartItem = context.ShoppingCarts.Where(x => x.FoodSetId == id && x.UserId == guid);
            if (shoppingCartItem.Count() == 0) return false;
            else return true;
        }
    }
}
