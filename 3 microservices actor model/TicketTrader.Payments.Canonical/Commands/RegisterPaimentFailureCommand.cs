using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Canonical.Commands
{
    public class RegisterPaimentFailureCommand : ICommand
    {
        public string PaymentId { get; set; }
    }
}
