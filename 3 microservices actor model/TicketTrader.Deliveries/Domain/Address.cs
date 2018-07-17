using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    public class Address : ValueObject
    {
        public string Recipient { get; protected set; }
        public string AddressLine { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }

        public Address(string recipient, string address, string city, string postalCode)
        {
            Recipient = recipient;
            AddressLine = address;
            City = city;
            PostalCode = postalCode;
        }
    }
}
