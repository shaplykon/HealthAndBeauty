﻿using HealthAndBeauty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<FoodSet> foodSets = context.FoodSets.Include(foodSet => foodSet.Ingredients).ToList();
            return foodSets;
        }

        public void AddFoodSet(FoodSet foodSet)
        {
            context.FoodSets.Add(foodSet);
            context.SaveChanges();
        }

        internal FoodSet GetFoodSetsById(int id)
        {
            var foodSet = context.FoodSets.Where(set => set.Id == id).Include(foodSet => foodSet.Ingredients).FirstOrDefault();
            return foodSet;
        }

        internal bool IsInShoppingCart(Guid userId, int id)
        {
            IQueryable<ShoppingCart> shoppingCartItem = context.ShoppingCarts.Where(x => x.FoodSetId == id && x.UserId == userId);
            return shoppingCartItem.Count() != 0;
        }

        internal List<Comment> GetCommentBySetId(int setId)
        {
            var commentsList = context.Comments.Where(comment => comment.FoodSetId == setId).ToList();
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

        internal void DeleteFoodSetById(int Id)
        {
            context.FoodSets.Remove(context.FoodSets.Where(foodSet => foodSet.Id == Id).FirstOrDefault());
            context.SaveChanges();
        }

    }
}
