using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating> GetRatingByIdAsync(string ratingId);
        Task<IEnumerable<Rating>> GetRatingsByVendorIdAsync(string vendorId);
        Task CreateRatingAsync(Rating rating);
        Task UpdateRatingAsync(Rating rating);
        Task DeleteRatingAsync(string ratingId);
    }
}
