using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Kernel;

namespace TicketTrader.Payments.Canonical.Commands
{
    public class RegisterNewPaymentCommand: ICommand
    {
        public string OrderId { get; set; }

        public string PaymentOptionId { get; set; }
    }
}
