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
        public DbSet<Address> Addresses { get; set; }
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

            ModelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    AddressName = "9 Gikalo str., Minsk, Belarus",
                    Latitude = 53.912254769620034,
                    Longtitude = 27.594474988468278,
                    Description = "Minsk"
                },
                new Address
                {
                    Id = 2,
                    AddressName = "65 Nezavisimosti Ave., Minsk",
                    Latitude = 53.92107236295033,
                    Longtitude = 27.592836631586675,
                    Description = "Minsk"
                }
                );
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
