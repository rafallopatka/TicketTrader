namespace TicketTrader.Orders.Domain.Actors
{
    class Reservation
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string EventId { get; set; }
        public string ReservationId { get; set; }
        public string SeatId { get; set; }
    }
}