using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Transify.Models;

namespace Transify.Controllers
{
    public class BusController : Controller
    {

        public IActionResult BusSchedules()
        {
            return View();
        }

    }
}