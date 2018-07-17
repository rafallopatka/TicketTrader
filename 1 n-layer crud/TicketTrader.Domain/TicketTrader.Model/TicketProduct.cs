using System.Collections.Generic;

namespace TicketTrader.Model
{
    public class TicketProduct
    {
        public int Id { get; set; }
        public PriceOption PriceOption { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public TicketProduct()
        {
            Reservations = new List<Reservation>();
        }
    }
}