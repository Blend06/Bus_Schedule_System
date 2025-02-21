using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Transify.Controllers
{

    [AllowAnonymous]
    public class TaxiController : Controller
    {
        public IActionResult TaxiBooking()
        {
            return View();
        }
        public IActionResult TaxiReservation()
        {
            return View();
        }
    }
}