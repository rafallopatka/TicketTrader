using System.Threading.Tasks;
using TicketTrader.Customers.Canonical.Queries;
using TicketTrader.Customers.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Customers.Application.Queries.Handlers
{
    class GetCustomerForUserQueryHandler: IQueryHandler<GetCustomerForUserQuery, GetCustomerForUserQuery.Response>
    {
        private readonly ICustomerFinder _customerFinder;

        public GetCustomerForUserQueryHandler(ICustomerFinder customerFinder)
        {
            _customerFinder = customerFinder;
        }

        public async Task<GetCustomerForUserQuery.Response> Handle(GetCustomerForUserQuery query)
        {
            var customer = await _customerFinder.GetCustomer(query.UserId);

            if (customer == null)
            {
                return new GetCustomerForUserQuery.Response();
            }

            return new GetCustomerForUserQuery.Response
            {
                Value = new GetCustomerForUserQuery.UserCustomerDto
                {
                    Address = customer.Address,
                    City = customer.City,
                    CustomerId = customer.Id,
                    Country = customer.Country,
                    Email = customer.Email,
                    FistName = customer.FistName,
                    UserId = customer.UserId,
                    LastName = customer.LastName,
                    PostalCode = customer.PostalCode
                }
            };
        }
    }
}
