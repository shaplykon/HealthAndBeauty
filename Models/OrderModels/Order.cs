using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAndBeauty.Models
{
    [Table("order")]
    public class Order: IValidatableObject
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourierId { get; set; }
        public string Status { get; set; }
        public DateTime ReceiptDate{ get; set; }
        public bool IsCash { get; set; }
        public bool IsDelivery { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
