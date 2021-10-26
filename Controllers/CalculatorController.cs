using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult BodyCalculatorIndex()
        {
            return View();
        }
    }
}
