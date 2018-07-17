namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondSelectedDelivery
    {
        public RespondSelectedDelivery(string id, string deliveryId)
        {
            Id = id;
            DeliveryId = deliveryId;
        }

        public string Id { get; }
        public string DeliveryId { get; }
    }
}