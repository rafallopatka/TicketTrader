using System;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Kernel;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class CommitOrderCommandHandler: ICommandHandler<CommitOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public CommitOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(CommitOrderCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));
            order.Commit();
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new OrderCommitedEvent(order.Id));
            await EventBus.Current.PublishAsync(new NewOrderCommitedEvent(order.Id, order.DeliveryMethod.Id, order.PaymentMethod.Id));
        }
    }
}
