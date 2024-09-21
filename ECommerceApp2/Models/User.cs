using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public enum UserRole
    {
        Administrator, //0
        Vendor, //1
        CSR //2
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // In production, passwords should be hashed!
        public UserRole Role { get; set; }
    }
}
