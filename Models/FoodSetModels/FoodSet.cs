using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("products")]
    public class FoodSet
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("price")]
        public int Price{ get; set; }
        [Column("delivery_time")]
        public int DeliveryTime{ get; set; }
        [Column("calorific")]
        public int Calorific { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string ImageData { get; set; }
        public bool IsActive { get; set; }
    }
}
