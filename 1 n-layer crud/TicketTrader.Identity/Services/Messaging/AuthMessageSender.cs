using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TicketTrader.Identity.Services.Messaging
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILogger<AuthMessageSender> _logger;

        public AuthMessageSender(ILogger<AuthMessageSender> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            LoggerExtensions.LogInformation(_logger, "Email: {email}, Subject: {subject}, Message: {message}", email, subject, message);
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            LoggerExtensions.LogInformation(_logger, "SMS: {number}, Message: {message}", number, message);
            return Task.FromResult(0);
        }
    }
}