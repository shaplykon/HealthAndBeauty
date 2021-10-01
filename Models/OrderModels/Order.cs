using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAndBeauty.Models
{
    [Table("order")]
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourierId { get; set; }
        public string Status { get; set; }
        public DateTime ReceiptDate{ get; set; }
        //public string DeliveryType { get; set; }
        //public string Address { get; set; }
    }
}
