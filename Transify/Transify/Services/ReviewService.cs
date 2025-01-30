using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Transify.Data;
using Transify.Interfaces;
using Transify.Models.Entities;

namespace Transify.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubmitReviewAsync(UserReview review, int? userId)
        {
            if (review == null)
                return false;

            string userName = "Anonymous";

            if (userId.HasValue)
            {
                var user = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == userId.Value);

                if (user != null)
                {
                    userName = user.FirstName;
                    review.UserId = userId.Value;
                }
            }

            review.UserName ??= userName;

            _context.Add(review);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}