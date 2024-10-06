/*
 * File Name: RatingService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IRatingService interface to manage ratings for vendors in the e-commerce application.
 *              This service provides functionality to:
 *              - Create a new rating for a specific vendor.
 *              - Update an existing rating, ensuring the vendor's average rating is recalculated.
 *              - Delete a rating and update the vendor's average rating accordingly.
 *              - Retrieve all ratings associated with a specific vendor by vendor ID.
 *              - Calculate the average rating of a vendor based on all associated ratings.
 *              The service utilizes injected repositories (IRatingRepository, IVendorRepository)
 *              to interact with the underlying data store and ensure proper rating management.
 */

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
