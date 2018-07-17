using System.Collections.Generic;

namespace TicketTrader.Model
{
    public abstract class Client
    {
        public int Id { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}