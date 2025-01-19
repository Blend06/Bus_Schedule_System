using Transify.Domain.Models.Entities;
using System.Threading.Tasks;

namespace Transify.Domain.Interfaces
{
    public interface IReviewService
    {
        Task<bool> SubmitReviewAsync(UserReview review, int? userId);
    }
}