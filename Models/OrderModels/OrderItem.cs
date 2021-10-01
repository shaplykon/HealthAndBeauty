using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAndBeauty.Models.OrderModels
{
    [Table("order_item")]
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodSetId { get; set; }
    }
}
