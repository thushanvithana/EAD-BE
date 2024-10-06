/*
 * File Name: RatingRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IRatingRepository interface for managing product ratings in the MongoDB database.
 *              This repository provides methods to:
 *              - Retrieve a rating by its ID
 *              - Retrieve ratings associated with a specific vendor ID
 *              - Create a new rating
 *              - Update an existing rating
 *              - Delete a rating by its ID
 *              Utilizes MongoDB.Driver for operations on the "Ratings" collection in the specified database.
 */

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
