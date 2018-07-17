using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class ReserveTicketCommandHandler: ICommandHandler<ReserveTicketCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public ReserveTicketCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(ReserveTicketCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));

            var ticketId = Id.From(command.TicketId);
            var eventId = Id.From(command.EventId);
            var seatId = Id.From(command.SceneSeatId);
            var prciceOptionId = Id.From(command.PriceOptionId);
            var seat = new Seat(seatId);

            var priceOption = new PriceOption(prciceOptionId, command.PriceOptionName, command.PriceZoneName, command.GrossAmount);
            var ticket = new Ticket(ticketId, eventId, seat, priceOption);

            order.AddTicket(ticket);
            await _orderRepository.Save(order);

            await EventBus.Current.PublishAsync(new TicketAddedEvent(order.Id, ticket.Id));
        }
    }
}