using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAndBeauty.Models
{
    [Table("shopping_cart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }
        public int FoodSetId { get; set; }
        public FoodSet FoodSet{ get; set; }
        public DateTime Date { get; set; }
        public bool isConfirmed { get; set; }
    }
}
