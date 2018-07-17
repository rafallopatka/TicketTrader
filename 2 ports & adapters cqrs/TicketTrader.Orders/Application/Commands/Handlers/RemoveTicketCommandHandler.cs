using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class RemoveTicketCommandHandler: ICommandHandler<RemoveTicketCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public RemoveTicketCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(RemoveTicketCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));
            var ticket = order.GetTicket(Id.From(command.TicketId));
            order.RemoveTicket(ticket);
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new TicketRemovedEvent(order.Id, ticket.Id));
        }
    }
}