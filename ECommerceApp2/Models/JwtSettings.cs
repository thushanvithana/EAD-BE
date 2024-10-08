/*
 * File Name: JwtSettings.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Model representing the settings for JSON Web Token (JWT) authentication.
 *              Contains properties such as the secret key, issuer, audience, and expiration time 
 *              for the JWT tokens.
 *              This model is used to configure JWT authentication in the e-commerce application.
 */
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
