using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Canonical.Commands
{
    public class RegisterPaymentSuccessCommand: ICommand
    {
        public string PaymentId { get; set; }
    }
}
