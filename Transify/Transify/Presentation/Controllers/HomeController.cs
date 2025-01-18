using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transify.Domain.Models;
using System.Diagnostics;
using Transify.Infrastructure.Persistence.Data.Transify.Infrastructure.Persistence.Data;

namespace Transify.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.UserReviews.AsNoTracking().ToListAsync();

            return View("~/Presentation/Views/Home/Index.cshtml", reviews);
        }

        public IActionResult Services()
        {
            return View("~/Presentation/Views/Home/Services.cshtml");
        }

        public IActionResult UserActivity()
        {
            return View("~/Presentation/Views/Home/UserActivity.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}