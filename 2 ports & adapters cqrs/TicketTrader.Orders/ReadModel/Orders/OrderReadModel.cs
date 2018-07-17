using TicketTrader.Orders.ReadModel.Shared;
using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Orders
{
    public class OrderReadModel: IReadModel
    {
        public string Id { get; set; }
        public OrderStateReadModel State { get; set; }
    }
}