using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("ingredients")]
    public class Ingredient
    {
        [Column("Id")]
        public int? Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public FoodSet FoodSet { get; set; }
    }
}
