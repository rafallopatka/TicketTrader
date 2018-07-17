namespace TicketTrader.Orders.Domain.Commands
{
    interface IOrderMessage
    {
        string OrderId { get; }
    }
}