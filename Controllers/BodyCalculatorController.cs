using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class BodyCalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
