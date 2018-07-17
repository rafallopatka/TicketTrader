using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Application.Commands
{
    public class RegisterNewDeliveryCommand: ICommand
    {
        public string OrderId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Recipient { get; set; }
        public string DeliveryTypeId { get; set; }
    }
}
