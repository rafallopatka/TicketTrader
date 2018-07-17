using System.Threading.Tasks;

namespace TicketTrader.Identity.Services.Messaging
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
