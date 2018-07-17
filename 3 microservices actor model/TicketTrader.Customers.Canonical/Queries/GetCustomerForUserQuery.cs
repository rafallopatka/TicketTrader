using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Customers.Canonical.Queries
{
    public class GetCustomerForUserQuery: IQuery
    {
        public string UserId { get; set; }

        public class Response: IQueryResponse
        {
            public UserCustomerDto Value { get; set; }
        }

        public class UserCustomerDto
        {
            public string CustomerId { get; set; }
            public string UserId { get; set; }

            public string FistName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string PostalCode { get; set; }
        }
    }
}
