using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("food_set")]
    public class FoodSet
    {
        [Column("Id")]
        public int Id { get; set; }
    }
}
