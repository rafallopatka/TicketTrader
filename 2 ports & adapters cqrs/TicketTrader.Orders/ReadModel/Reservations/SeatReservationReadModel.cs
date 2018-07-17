using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Reservations
{
    public class SeatReservationReadModel: IReadModel
    {
        public string Id { get; set; }
        public string SceneSeatId { get; set; }
        public string EventId { get; set; }
        public string OrderId { get; set; }
    }
}