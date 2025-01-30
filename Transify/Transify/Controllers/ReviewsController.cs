using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Transify.Interfaces;
using Transify.Models.Entities;
using Transify.Data;

namespace Transify.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReviewService _reviewService;

        public ReviewsController(AppDbContext context, IHttpContextAccessor httpContextAccessor, IReviewService reviewService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.UserReviews.ToListAsync();
            return View("Index", reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReview(UserReview review)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
                var result = await _reviewService.SubmitReviewAsync(review, userId);

                if (result)
                    return RedirectToAction("Index", "Home");
            }

            return View("~/Presentation/Views/Home/Index.cshtml", review);
        }

        [HttpGet]
        public IActionResult SubmitReview()
        {
            return View(new UserReview());
        }
    }
}