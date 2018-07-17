using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    public class User: DomainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}