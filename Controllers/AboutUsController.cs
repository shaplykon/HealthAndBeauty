using Flurl.Http;
using HealthAndBeauty.Data.Repositories;
using HealthAndBeauty.Models;
using HealthAndBeauty.Models.JsonApiModels;
using HealthAndBeauty.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class AboutUsController : Controller
    {
        GoogleMapsRepository _repository;
        public AboutUsController(GoogleMapsRepository repository)
        {
            _repository = repository;
        }
        public IActionResult AboutUs()
        {
            ViewBag.Addresses = _repository.GetAddresses().ToList();

            return View();
        }

        public JsonResult GetMapMarker()
        {
            var ListOfAddress = _repository.GetCoordinates().ToList();

            return Json(ListOfAddress);
        }
        public JsonResult GetMapMarkersForEdit()
        {
            List<Address> ListOfAddress = new List<Address>();

            if (User.IsInRole("admin") || User.IsInRole("manager"))
                ListOfAddress = _repository.GetCoordinates().ToList();

            return Json(ListOfAddress);
        }


        [Authorize(Roles = "admin, manager")]
        public IActionResult EditLocations()
        {
            ViewBag.Addresses = _repository.GetCoordinates().ToList();
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAddress(int id)
        {
            _repository.DeleteAddress(id);
            return RedirectToAction("EditLocations");
        }

        [HttpPost]
        public IActionResult AddressAdd(AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                Address address = (Address)addressViewModel;
                _repository.AddAddress(address);
            }
            return RedirectToAction("EditLocations");
        }

        [HttpGet]
        [Route("AboutUs/GetAddressByCoords/{lat:double}/{lon:double}")]
        public async Task<string> GetAddressByCoords(double lat, double lon)
        {
            string latitude = lat.ToString().Replace(',', '.');
            string longtitude = lon.ToString().Replace(',', '.');
            string WEBSERVICE_URL = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longtitude}&sensor=true_or_false&key=AIzaSyCJMF3gVJD6f6ue_2lLeBODZIfgOI2B3os&result_type=street_address&language=en";
            AddressInfo addressArray = null;

            try
            {
                addressArray = await WEBSERVICE_URL
                    .PostAsync().ReceiveJson<AddressInfo>();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return addressArray.AddressResults.FirstOrDefault()?.FormattedAddress;//addressArray.AddressResults.Le addressArray.AddressResults.First().; 
        }
    }
}
