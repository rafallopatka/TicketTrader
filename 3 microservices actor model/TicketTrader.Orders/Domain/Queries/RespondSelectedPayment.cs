
namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondSelectedPayment
    {
        public RespondSelectedPayment(string id, string paymentId)
        {
            Id = id;
            PaymentId = paymentId;
        }

        public string Id { get; }
        public string PaymentId { get; }
    }
}
