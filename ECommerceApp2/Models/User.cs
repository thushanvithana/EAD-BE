/*
 * File Name: User.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Defines the User model with properties that map to the user-related data 
 *              in the application, including fields like UserID, Username, Email, etc.
 */
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

    public enum Gender
    {
        Male, //0
        Female,//1
        Other,//2
        PreferNotToSay//3
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

        // New Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } // Changed from Address object to single string
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

   
    }
}
