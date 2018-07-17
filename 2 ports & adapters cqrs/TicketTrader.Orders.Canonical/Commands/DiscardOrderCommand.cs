using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Commands
{
    public class DiscardOrderCommand: ICommand
    {
        public string OrderId { get; set; }
    }
}
