// Services/Interfaces/IEmailService.cs
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
