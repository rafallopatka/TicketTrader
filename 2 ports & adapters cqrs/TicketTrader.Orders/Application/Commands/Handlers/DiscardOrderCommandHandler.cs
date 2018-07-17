using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class DiscardOrderCommandHandler: ICommandHandler<DiscardOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public DiscardOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(DiscardOrderCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));
            order.Discard();
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new OrderDiscardedEvent(order.Id));
        }
    }
}