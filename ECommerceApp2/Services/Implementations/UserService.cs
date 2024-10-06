/*
 * File Name: UserService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implements the IUserService interface, providing the concrete logic
 *              for user-related business operations, including validation and service calls.
 */

using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ECommerceApp2.Controllers.UserController;

namespace ECommerceApp2.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<User> Register(User user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new System.Exception("User already exists");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Set IsActive based on Role
            user.IsActive = user.Role != UserRole.Customer;

            await _userRepository.AddUser(user); // ID is generated automatically

            // Send confirmation email with stylish HTML
            var subject = "Registration Successful";
            var message = GetStylishEmailBody(user.FirstName);
            await _emailService.SendEmailAsync(user.Email, subject, message);

            return user;
        }

        private string GetStylishEmailBody(string firstName)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Registration Successful</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        /* General Styles */
        body {{
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
        }}
        .email-container {{
            max-width: 600px;
            margin: auto;
            background-color: #ffffff;
        }}
        .header {{
            background-color: #007bff;
            padding: 20px;
            text-align: center;
        }}
        .header img {{
            max-width: 150px;
            height: auto;
        }}
        .content {{
            padding: 20px;
            color: #333333;
        }}
        .content h2 {{
            color: #007bff;
        }}
        .cta-button {{
            display: inline-block;
            padding: 10px 20px;
            margin-top: 20px;
            background-color: #28a745;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
        }}
        .footer {{
            background-color: #f1f1f1;
            padding: 20px;
            text-align: center;
            font-size: 12px;
            color: #777777;
        }}
        /* Responsive Styles */
        @media only screen and (max-width: 600px) {{
            .email-container {{
                width: 100% !important;
            }}
            .header, .content, .footer {{
                padding: 10px !important;
            }}
            .cta-button {{
                width: 100% !important;
                text-align: center !important;
            }}
        }}
    </style>
</head>
<body>
    <table class=""email-container"" cellpadding=""0"" cellspacing=""0"">
        <!-- Header -->
        <tr>
            <td class=""header"">
                <img src=""https://yourdomain.com/logo.png"" alt=""Your App Logo"">
            </td>
        </tr>
        <!-- Content -->
        <tr>
            <td class=""content"">
                <h2>Welcome, {firstName}!</h2>
                <p>Thank you for registering at <strong>ECommerceApp2</strong>. We're excited to have you on board.</p>
                <p>Get started by exploring our wide range of products tailored just for you.</p>
                <a href=""https://yourdomain.com/login"" class=""cta-button"">Shop Now</a>
            </td>
        </tr>
        <!-- Footer -->
        <tr>
            <td class=""footer"">
                <p>&copy; 2024 ECommerceApp2. All rights reserved.</p>
                <p>If you have any questions, feel free to contact us at <a href=""mailto:support@yourdomain.com"">support@yourdomain.com</a>.</p>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new System.Exception("Invalid email or password");
            }

            if (!user.IsActive)
            {
                throw new System.Exception("User account is inactive");
            }

            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }


        public async Task<IEnumerable<User>> GetActivatedUsers()
        {
            return await _userRepository.GetActivatedUsers();
        }

        public async Task<IEnumerable<User>> GetDeactivatedUsers()
        {
            return await _userRepository.GetDeactivatedUsers();
        }

        public async Task DeleteUser(string id)
        {
            await _userRepository.DeleteUser(id);
        }

        // New Methods for Activating/Deactivating Users
        public async Task ActivateUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new System.Exception("User not found");
            }

            user.IsActive = true;
            await _userRepository.UpdateUser(user);
        }


        public async Task DeactivateUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new System.Exception("User not found");
            }

            user.IsActive = false;
            await _userRepository.UpdateUser(user);
        }


        public async Task UpdateUser(string id, UpdateUserRequest updateRequest)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new System.Exception("User not found");
            }

            // Update fields except Password
            user.Username = updateRequest.Username;
            user.Email = updateRequest.Email;
            user.Role = updateRequest.Role;
            user.FirstName = updateRequest.FirstName;
            user.LastName = updateRequest.LastName;
            user.Address = updateRequest.Address;
            user.PhoneNumber = updateRequest.PhoneNumber;
            user.Gender = updateRequest.Gender;

            await _userRepository.UpdateUser(user);
        }
    }
}
