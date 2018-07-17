using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Payments.ReadModel
{
    public class PaymentReadModel: IReadModel
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public string OptionId { get; set; }
        public PaymentStatus Status { get; set; }

        public enum PaymentStatus
        {
            Awaiting,
            Completed,
            Failed
        }
    }
}
