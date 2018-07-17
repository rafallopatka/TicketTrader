using System.Threading.Tasks;
using TicketTrader.Customers.Canonical.Commands;
using TicketTrader.Customers.Canonical.Events;
using TicketTrader.Customers.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Application.Commands.Handlers
{
    class CreateCustomerForUserHandler : ICommandHandler<CreateCustomerForUserCommand>
    {
        private readonly IRepository<Customer> _repository;
        private readonly CustomerFactory _factory;

        public CreateCustomerForUserHandler(IRepository<Customer> repository, CustomerFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task HandleAsync(CreateCustomerForUserCommand command)
        {
            var user = new User
            {
                Id = Id.From(command.UserId),
                Address = command.Address,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Country = command.Country,
                Email = command.Email,
                PostalCode = command.PostalCode,
                City = command.City,
            };

            var customerId = Id.From(command.CustomerId);
            var customer = _factory.Create(customerId, user);
            await _repository.Save(customer);
            await EventBus.Current.PublishAsync(new CustomerCreatedEvent(customerId));
        }
    }
}
