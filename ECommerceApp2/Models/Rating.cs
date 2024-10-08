/*
 * File Name: Rating.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing a rating given by a customer to a vendor in the e-commerce application.
 *              Contains properties for the rating ID, vendor ID, customer ID, star rating (1 to 5), and a comment.
 *              The rating ID is automatically generated using a MongoDB ObjectId to ensure uniqueness.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VendorId { get; set; }
        public string CustomerId { get; set; }
        public int Stars { get; set; } // 1 to 5
        public string Comment { get; set; }

        // Constructor to automatically generate an ID
        public Rating()
        {
            Id = ObjectId.GenerateNewId().ToString(); // Generate a new unique ObjectId
        }
    }
}
