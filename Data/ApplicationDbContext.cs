using HealthAndBeauty.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthAndBeauty.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FoodSet> FoodSets { get; set; }
        public DbSet<MapsCoordinates> MapsCoordinates { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            ModelBuilder.Entity<FoodSet>().HasData(
            new FoodSet[]
            {
                new FoodSet { Id = 1 },
                new FoodSet { Id = 2 }
            });

            ModelBuilder.Entity<MapsCoordinates>().HasData(
                new MapsCoordinates
                {
                    Id = 1,
                    AddressName = "SF",
                    Latitude = 37.77,
                    Longtitude = -122.41,
                    Description = "San Francisco"
                });
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
                });
        }
    }
}
