using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CompleteOrderCommandHandler: ICommandHandler<CompleteOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public CompleteOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(CompleteOrderCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));
            order.CompleteOrder();
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new OrderCompletedEvent(order.Id));
        }
    }
}