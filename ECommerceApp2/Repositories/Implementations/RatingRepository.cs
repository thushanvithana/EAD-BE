
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;

namespace ECommerceApp2.Repositories.Implementations
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IMongoCollection<Rating> _ratings;

        public RatingRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _ratings = database.GetCollection<Rating>("Ratings");
        }

        public async Task<Rating> GetRatingByIdAsync(string ratingId)
        {
            return await _ratings.Find(r => r.Id == ratingId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByVendorIdAsync(string vendorId)
        {
            return await _ratings.Find(r => r.VendorId == vendorId).ToListAsync();
        }

        public async Task CreateRatingAsync(Rating rating)
        {
            await _ratings.InsertOneAsync(rating);
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            await _ratings.ReplaceOneAsync(r => r.Id == rating.Id, rating);
        }

        public async Task DeleteRatingAsync(string ratingId)
        {
            await _ratings.DeleteOneAsync(r => r.Id == ratingId);
        }
    }
}
