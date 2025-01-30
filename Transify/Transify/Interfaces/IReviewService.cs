using System.Threading.Tasks;
using Transify.Models.Entities;

namespace Transify.Interfaces
{
    public interface IReviewService
    {
        Task<bool> SubmitReviewAsync(UserReview review, int? userId);
    }
}