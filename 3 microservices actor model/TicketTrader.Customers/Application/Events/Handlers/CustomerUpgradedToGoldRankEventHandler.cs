using System.Threading.Tasks;
using TicketTrader.Customers.Domain;
using TicketTrader.Customers.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Application.Events.Handlers
{
    class CustomerUpgradedToGoldRankEventHandler : IEventHandler<CustomerUpgradedToGoldRankEvent>
    {
        private readonly ICustomerDenormalizer _denormalizer;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerUpgradedToGoldRankEventHandler(ICustomerDenormalizer denormalizer, IRepository<Customer> customerRepository)
        {
            _denormalizer = denormalizer;
            _customerRepository = customerRepository;
        }

        async Task IEventHandler<CustomerUpgradedToGoldRankEvent>.Handle(CustomerUpgradedToGoldRankEvent domainEvent)
        {
            var customer = await _customerRepository.Get(domainEvent.CustomerId);

            await _denormalizer.UpdateCustomerAsync(customer);
        }
    }
}