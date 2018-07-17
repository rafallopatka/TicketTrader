using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class SelectPaymentCommandHandler: ICommandHandler<SelectPaymentCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public SelectPaymentCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(SelectPaymentCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));

            var paymentMethod = new PaymentMethod(Id.From(command.OrderId), Id.From(command.PaymentTypeId));

            order.SetPaymentMethod(paymentMethod);
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new PaymentMethodSetEvent(order.Id, paymentMethod.Id));
        }
    }
}