using System.Threading.Tasks;
using TicketTrader.Customers.Domain;
using TicketTrader.Customers.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Application.Events.Handlers
{
    class CustomerUpgradedToStandardRankEventHandler : IEventHandler<CustomerUpgradedToStandardRankEvent>
    {
        private readonly ICustomerDenormalizer _denormalizer;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerUpgradedToStandardRankEventHandler(ICustomerDenormalizer denormalizer, IRepository<Customer> customerRepository)
        {
            _denormalizer = denormalizer;
            _customerRepository = customerRepository;
        }

        async Task IEventHandler<CustomerUpgradedToStandardRankEvent>.Handle(CustomerUpgradedToStandardRankEvent domainEvent)
        {
            var customer = await _customerRepository.Get(domainEvent.CustomerId);

            await _denormalizer.UpdateCustomerAsync(customer);
        }
    }
}