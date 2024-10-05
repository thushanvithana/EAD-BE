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
