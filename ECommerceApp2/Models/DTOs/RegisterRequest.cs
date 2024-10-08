/*
 * File Name: RegisterRequest.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing a registration request for new users.
 *              Contains user registration details such as username, email, password, role, 
 *              and additional information like first name, last name, address, phone number, and gender.
 *              This DTO is used for transferring registration data between the client and the API.
 */
namespace ECommerceApp2.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        // New Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } // Changed to single string
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
