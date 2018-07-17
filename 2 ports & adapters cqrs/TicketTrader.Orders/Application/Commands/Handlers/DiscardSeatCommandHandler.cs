using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class DiscardSeatCommandHandler: ICommandHandler<DiscardSeatCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public DiscardSeatCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(DiscardSeatCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));

            var reservationId = Id.From(command.ReservationId);
            var eventId = Id.From(command.EventId);
            var seatId = Id.From(command.SceneSeatId);
            var reservation = new Reservation(reservationId, eventId, new Seat(seatId));

            order.DicardReservation(reservation);
            await _orderRepository.Save(order);
            await EventBus.Current.PublishAsync(new ReservationDiscardedEvent(order.Id, reservation.Id, reservation.Seat.Id));
        }
    }
}