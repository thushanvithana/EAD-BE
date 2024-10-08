/*
 * File Name: CartItem.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing an item in the shopping cart of the e-commerce application.
 *              Contains properties such as product ID and quantity of the product.
 *              This model is used for managing individual items within a user's shopping cart.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
