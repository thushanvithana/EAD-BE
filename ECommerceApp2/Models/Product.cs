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
