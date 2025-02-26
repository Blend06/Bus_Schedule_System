using Microsoft.AspNetCore.Mvc;

namespace Transify.Controllers
{
    [Route("PrivacyPolicy")]
    public class PrivacyPolicyController : Controller
    {

        [HttpGet("", Name = "PrivacyPolicyIndex")]
        public IActionResult Index()
        {
            return View();
        }
    }
}