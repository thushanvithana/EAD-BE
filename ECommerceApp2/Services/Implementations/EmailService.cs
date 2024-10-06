﻿// Services/Implementations/EmailService.cs
/*
 * File Name: EmailService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IEmailService interface for sending emails in the e-commerce application.
 *              This service provides functionality to:
 *              - Send emails asynchronously using SMTP.
 *              The service is configured with email settings including SMTP server details and sender information.
 *              In case of errors during email sending, exceptions are handled and logged appropriately.
 */
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSettings _emailSettings;

        public EmailService(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            mail.To.Add(new MailAddress(toEmail));
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true; // Set to true if the message is HTML

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };

            try
            {
                await smtpClient.SendMailAsync(mail);
                System.Console.WriteLine("Sent");
            }
            catch (SmtpException ex)
            {
                // Handle exception (e.g., log it)
                throw new System.Exception($"An error occurred while sending email: {ex.Message}");
            }
        }
    }
}
