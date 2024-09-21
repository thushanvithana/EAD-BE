
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IVendorRepository _vendorRepository;

        public RatingService(IRatingRepository ratingRepository, IVendorRepository vendorRepository)
        {
            _ratingRepository = ratingRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task CreateRatingAsync(Rating rating)
        {
            await _ratingRepository.CreateRatingAsync(rating);
            await UpdateVendorAverageRatingAsync(rating.VendorId);
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            await _ratingRepository.UpdateRatingAsync(rating);
            await UpdateVendorAverageRatingAsync(rating.VendorId);
        }

        public async Task DeleteRatingAsync(string ratingId)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(ratingId);
            if (rating != null)
            {
                await _ratingRepository.DeleteRatingAsync(ratingId);
                await UpdateVendorAverageRatingAsync(rating.VendorId);
            }
        }

        public async Task<IEnumerable<Rating>> GetRatingsByVendorIdAsync(string vendorId)
        {
            return await _ratingRepository.GetRatingsByVendorIdAsync(vendorId);
        }

        public async Task<double> GetAverageRatingByVendorIdAsync(string vendorId)
        {
            var ratings = await _ratingRepository.GetRatingsByVendorIdAsync(vendorId);
            if (ratings.Any())
            {
                return ratings.Average(r => r.Stars);
            }
            return 0;
        }

        private async Task UpdateVendorAverageRatingAsync(string vendorId)
        {
            var averageRating = await GetAverageRatingByVendorIdAsync(vendorId);
            var vendor = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendor != null)
            {
                vendor.AverageRating = averageRating;
                await _vendorRepository.UpdateVendorAsync(vendor);
            }
        }
    }
}
