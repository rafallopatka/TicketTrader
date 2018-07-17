using System.Collections.Generic;

namespace TicketTrader.Model
{
    public abstract class Delivery
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Price ServiceFee { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Delivery()
        {
            Orders = new List<Order>();
        }
    }
}