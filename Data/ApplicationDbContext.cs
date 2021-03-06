using HealthAndBeauty.Models;
using HealthAndBeauty.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthAndBeauty.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<FoodSet> FoodSets { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<History> History { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            //ModelBuilder.Entity<Ingredient>().HasOne(c => c.FoodSet).WithMany(c => c.Ingredients).IsRequired(true);

            ModelBuilder.Entity<FoodSet>().HasMany(c => c.Ingredients).WithOne(c => c.FoodSet).IsRequired(true);

            ModelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "user",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "4",
                    Name = "courier",
                    NormalizedName = "COURIER"
                });
        }
    }
}
