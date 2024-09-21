using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class Product
    {
        [BsonId]
        public string ProductId { get; set; } // Unique Product ID provided by Vendor

        public string Name { get; set; }

        public string Category { get; set; }

        public string VendorId { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }
    }
}
