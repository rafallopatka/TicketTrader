using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Customers.Canonical.Commands
{
    public class CreateCustomerForUserCommand: ICommand
    {
        public string UserId { get; set; }
        public string CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
