using HealthAndBeauty.Models;

using Microsoft.AspNetCore.Identity;
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

        internal bool IsInShoppingCart(Guid userId, int id)
        {
            IQueryable<ShoppingCart> shoppingCartItem = context.ShoppingCarts.Where(x => x.FoodSetId == id && x.UserId == userId);
            if (shoppingCartItem.Count() == 0) return false;
            else return true;
        }

        internal List<Comment> GetCommentBySetId(int setId)
        {
            var commentsList = context.Comments.Where(comment => comment.FoodSetId == setId).ToList();

            foreach(Comment comment in commentsList)
            {
                
            }
            return commentsList;
        }

        internal void AddComment(Comment comment, int foodSetId)
        { 
            comment.FoodSetId = context.FoodSets.Where(set => set.Id == foodSetId).FirstOrDefault().Id;
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        internal void DeleteCommentById(int commentId)
        {
            context.Comments.Remove(context.Comments.Where(comment => comment.Id == commentId).FirstOrDefault());
            context.SaveChanges();
        }

        internal void DeleteFromShoppingCart(Guid userId, int foodSetId)
        {
            context.ShoppingCarts.Remove(context.ShoppingCarts.Where(cart => cart.UserId == userId && cart.FoodSetId == foodSetId).FirstOrDefault());
            context.SaveChanges();
        }

        internal void AddToShoppingCart(ShoppingCart shoppingCart)
        {
            context.ShoppingCarts.Add(shoppingCart);
            context.SaveChanges();
        }

        internal List<ShoppingCart> GetShoppingCartByUserId(Guid userId)
        {
            return context.ShoppingCarts.Where(cart => cart.UserId == userId).ToList();
        }

    }
}
