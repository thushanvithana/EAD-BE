
/*
 * File Name: Cart.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing a shopping cart in the e-commerce application.
 *              Contains properties such as the cart ID, user ID, and a list of items in the cart.
 *              The cart ID is represented as an ObjectId in MongoDB.
 *              This model is used for interacting with the cart data in the database.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ECommerceApp2.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
