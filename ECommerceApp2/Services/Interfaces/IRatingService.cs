
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IRatingService
    {
        Task CreateRatingAsync(Rating rating);
        Task UpdateRatingAsync(Rating rating);
        Task DeleteRatingAsync(string ratingId);
        Task<IEnumerable<Rating>> GetRatingsByVendorIdAsync(string vendorId);
        Task<double> GetAverageRatingByVendorIdAsync(string vendorId);
    }
}
