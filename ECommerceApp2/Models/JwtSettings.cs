namespace ECommerceApp2.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; } // Secret key for JWT
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
