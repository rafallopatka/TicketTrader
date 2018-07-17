using System.Threading.Tasks;
using TicketTrader.Customers.Canonical.Events;
using TicketTrader.Customers.Domain;
using TicketTrader.Customers.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Application.Events.Handlers
{
    class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        private readonly ICustomerDenormalizer _denormalizer;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerCreatedEventHandler(ICustomerDenormalizer denormalizer, IRepository<Customer> customerRepository)
        {
            _denormalizer = denormalizer;
            _customerRepository = customerRepository;
        }


        public async Task Handle(CustomerCreatedEvent domainEvent)
        {
            var customer = await _customerRepository.Get(domainEvent.CustomerId);

            await _denormalizer.AddCustomer(customer);
        }
    }
}
