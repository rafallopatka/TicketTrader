
using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Deliveries.ReadModel
{
    public class DeliveryReadModel: IReadModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string DeliveryTypeId { get; set; }
        public DeliveryStatus Status { get; set; }

        public enum DeliveryStatus
        {
            Awaiting,
            Completed,
            Failed,
            Canceled
        }
    }
}
