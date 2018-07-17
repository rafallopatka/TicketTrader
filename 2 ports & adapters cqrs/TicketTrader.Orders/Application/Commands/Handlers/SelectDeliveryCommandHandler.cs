using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class SelectDeliveryCommandHandler: ICommandHandler<SelectDeliveryCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public SelectDeliveryCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(SelectDeliveryCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));

            var deliveryMethod = new DeliveryMethod(Id.From(command.OrderId), Id.From(command.DeliveryTypeId));

            order.SetDeliveryMethod(deliveryMethod);
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new DeliveryMethodSetEvent(order.Id, deliveryMethod.Id));
        }
    }
}