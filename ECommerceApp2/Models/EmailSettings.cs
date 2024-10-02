// Models/EmailSettings.cs
namespace ECommerceApp2.Models
{
    public class EmailSettings : IEmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public interface IEmailSettings
    {
        string SmtpServer { get; set; }
        int SmtpPort { get; set; }
        string SenderName { get; set; }
        string SenderEmail { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
