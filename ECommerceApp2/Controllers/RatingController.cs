using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        // Dependency Injection
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // GET api/rating/vendor/{vendorId}
        [HttpGet("vendor/{vendorId}")]
        public async Task<IActionResult> GetRatingsByVendor(string vendorId)
        {
            var ratings = await _ratingService.GetRatingsByVendorIdAsync(vendorId);
            return Ok(ratings);
        }

        // GET api/rating/vendor/{vendorId}/average
        [HttpGet("vendor/{vendorId}/average")]
        public async Task<IActionResult> GetAverageRating(string vendorId)
        {
            var averageRating = await _ratingService.GetAverageRatingByVendorIdAsync(vendorId);
            return Ok(averageRating);
        }

        // POST api/rating
        [HttpPost]
        public async Task<IActionResult> CreateRating(Rating rating)
        {
            await _ratingService.CreateRatingAsync(rating);
            return Ok("Rating created successfully.");
        }

        // PUT api/rating/{ratingId}
        [HttpPut("{ratingId}")]
        public async Task<IActionResult> UpdateRating(string ratingId, Rating updatedRating)
        {
            var existingRating = await _ratingService.GetRatingsByVendorIdAsync(updatedRating.VendorId);
            if (existingRating == null)
                return NotFound();

            updatedRating.Id = ratingId;
            await _ratingService.UpdateRatingAsync(updatedRating);
            return Ok("Rating updated successfully.");
        }

        // DELETE api/rating/{ratingId}
        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeleteRating(string ratingId)
        {
            await _ratingService.DeleteRatingAsync(ratingId);
            return Ok("Rating deleted successfully.");
        }
    }
}
