/*
 * File Name: Product.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing a product in the e-commerce application.
 *              Contains properties such as product ID, name, category, vendor ID,
 *              active status, price, description, stock quantity, and a list of image URLs.
 *              The product ID is generated using a GUID by default, and the active status
 *              is set to true upon creation.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class Product
    {
        [BsonId]
        public string ProductId { get; set; } = Guid.NewGuid().ToString(); // Generate a new GUID by default

        public string Name { get; set; }

        public string Category { get; set; }

        public string VendorId { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        // New property for product images
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
