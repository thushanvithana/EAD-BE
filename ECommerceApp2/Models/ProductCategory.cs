using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class ProductCategory
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        // Constructor to automatically generate an ID
        public ProductCategory()
        {
            Id = Guid.NewGuid().ToString(); // Generate a new unique ID
        }
    }
}
