/*
 * File Name: UpdateUserRequest.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing a request to update user information.
 *              Contains fields such as username, email, role, first name, last name, address, 
 *              phone number, gender, and active status.
 *              This DTO is used for transferring user update data between the client and the API.
 *              The password field is intentionally excluded to prevent editing.
 */
using ECommerceApp2.Models;

namespace ECommerceApp2.Models.DTOs
{
    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        // New Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } // Single string
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

        public bool IsActive { get; set; }

        // Password field is intentionally excluded to prevent editing
    }
}
