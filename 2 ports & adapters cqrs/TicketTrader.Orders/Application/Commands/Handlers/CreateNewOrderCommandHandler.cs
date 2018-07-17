using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CreateNewOrderCommandHandler: ICommandHandler<CreateNewOrderCommand>
    {
        private readonly IRepository<Order> _ordeRepository;
        private readonly OrderFactory _factory;

        public CreateNewOrderCommandHandler(IRepository<Order> ordeRepository, OrderFactory factory)
        {
            _ordeRepository = ordeRepository;
            _factory = factory;
        }

        public async Task HandleAsync(CreateNewOrderCommand command)
        {
            var orderId = Id.From(command.OrderId);
            var clientId = Id.From(command.ClientId);

            Order order = await _factory.CreateOrder(orderId, clientId);
            await _ordeRepository.Save(order);
            var paymentEvent = new OrderCreatedEvent(order.Id, order.Client.Id, CurrentDateTime.Local);
            await EventBus.Current.PublishAsync(paymentEvent);
        }
    }
}