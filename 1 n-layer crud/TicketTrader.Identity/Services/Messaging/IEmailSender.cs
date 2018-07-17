using System.Threading.Tasks;

namespace TicketTrader.Identity.Services.Messaging
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}