using HealthAndBeauty.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace HealthAndBeauty.ViewModels
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please input cash")]
        public bool IsCash { get; set; }

        [Required(ErrorMessage = "Please input delivery")]
        public bool IsDelivery { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressName { get; set; }

        public static explicit operator Order(OrderViewModel viewModel)
        {
            Order order = new Order();

            if (viewModel.IsCash != default)
            {
                order.IsCash = viewModel.IsCash;
            }
            if (viewModel.IsDelivery != default)
            {
                order.IsDelivery = viewModel.IsDelivery;
            }
            return order;
        }
    }
}
