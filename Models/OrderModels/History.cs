using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models.OrderModels
{
    [Table("History")]
    public class History
    {
        public int Id { get; set; }
        public string CurrentStatus { get; set; }
        public Guid UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
