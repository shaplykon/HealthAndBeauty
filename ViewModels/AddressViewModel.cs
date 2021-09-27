using HealthAndBeauty.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.ViewModels
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "Please input latitude")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "Please input longtitutde")]
        public string Longtitude { get; set; }

        [Required(ErrorMessage = "Please input address")]
        public string AddressName{ get; set; }

        //[Required(ErrorMessage = "Please input description")]
        public string Description { get; set; }

        public static explicit operator Address(AddressViewModel viewModel)
        {
            Address address = new Address();

            if (viewModel.AddressName != default)
            {
                address.AddressName = viewModel.AddressName;
            }
            if (viewModel.Description != default)
            {
                address.Description = viewModel.Description;
            }
            if (viewModel.Latitude != default)
            {
                address.Latitude = double.Parse(viewModel.Latitude.Replace('.', ','));
            }
            if (viewModel.Longtitude != default)
            {
                address.Longtitude = double.Parse(viewModel.Longtitude.Replace('.', ','));
            }
 
            return address;
        }
    }
}
