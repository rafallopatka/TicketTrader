namespace TicketTrader.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public bool Discarded { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }    

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? TicketId { get; set; }
        public TicketProduct Ticket { get; set; }
    }
}