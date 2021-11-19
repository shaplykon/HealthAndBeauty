using HealthAndBeauty.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace HealthAndBeauty.ViewModels
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please select payment method")]
        public bool IsCash { get; set; }

        [Required(ErrorMessage = "Please select delivery method")]
        public bool IsDelivery { get; set; }

        [Required(ErrorMessage = "Please input correct delivery address")]
        public string Address { get; set; }
        [Phone]
        [Required(ErrorMessage = "Please input phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please input correct credit card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please input correct pickup  address")]
        public string PickupAddress { get; set; }

/*        public static explicit operator Order(OrderViewModel viewModel)
        {
            Order order = new Order();

            if (viewModel.IsCash != default)            
                order.IsCash = viewModel.IsCash;
            
            if (viewModel.IsDelivery != default)
                order.IsDelivery = viewModel.IsDelivery;
            
            if(viewModel.CardNumber != default)            
                order.CardNumber = viewModel.CardNumber;

            if (viewModel.Address != default && viewModel.IsDelivery)
                order.Address = viewModel.Address;

            if (viewModel.PickupAddress != default && !viewModel.IsDelivery)
                order.Address = viewModel.PickupAddress;

            return order;
        }*/
    }
}
