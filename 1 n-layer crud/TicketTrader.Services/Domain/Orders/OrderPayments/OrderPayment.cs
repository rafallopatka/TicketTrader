namespace TicketTrader.Services.Domain.Orders.OrderPayments
{
    public class OrderPaymentDto
    {
        public int OrderId { get; set; }
        public int? PaymentId { get; set; }
    }
}