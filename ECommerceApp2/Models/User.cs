using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ECommerceApp2.Models
{
    public enum UserRole
    {
        Administrator, //0
        Vendor,        //1
        CSR,           //2
        Customer       //3
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString(); // Automatically generate a new ObjectId

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } // New Field

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
