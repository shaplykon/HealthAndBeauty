using HealthAndBeauty.Data.Repositories;
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
            return View();
        }

        public JsonResult GetMapMarker()
        {
            var ListOfAddress = _repository.GetCoordinates().ToList();

            return Json(ListOfAddress);
        }

    }
}
