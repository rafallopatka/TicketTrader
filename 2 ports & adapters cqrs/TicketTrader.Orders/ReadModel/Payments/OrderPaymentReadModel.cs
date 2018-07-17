using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Payments
{
    public class OrderPaymentReadModel: IReadModel
    {
        public string Id { get; set; }
        public string PaymentId { get; set; }
    }
}
