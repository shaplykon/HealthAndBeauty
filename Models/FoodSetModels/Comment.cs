using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("comment")]
    public class Comment
    {
        public int Id { get; set; }
        [Required, Display(Name = "Comment text")]
        public string Text { get; set; }
        [Required, Display(Name = "Short description")]
        public string ShortDescription { get; set; }
        public DateTime Date { get; set; }
        public string  UserId { get; set; }
        public int FoodSetId { get; set; }

    }
}
