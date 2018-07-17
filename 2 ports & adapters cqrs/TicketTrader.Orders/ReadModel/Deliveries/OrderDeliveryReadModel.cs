using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Deliveries
{
    public class OrderDeliveryReadModel: IReadModel
    {
        public string Id { get; set; }
        public string DeliveryId { get; set; }
    }
}