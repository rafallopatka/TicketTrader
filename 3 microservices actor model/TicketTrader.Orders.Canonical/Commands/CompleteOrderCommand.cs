using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class CompleteOrderCommand: ICommand
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
    }
}
