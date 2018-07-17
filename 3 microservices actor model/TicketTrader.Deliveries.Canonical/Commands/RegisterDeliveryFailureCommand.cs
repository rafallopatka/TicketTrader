using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Canonical.Commands
{
    public class RegisterDeliveryFailureCommand: ICommand
    {
        public string DeliveryId { get; set; }
    }
}
