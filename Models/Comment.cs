using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("comment")]
    public class COmment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ShortDescription { get; set; }
        public DateTime Date { get; set; }
        public IdentityUser User { get; set; }

    }
}
