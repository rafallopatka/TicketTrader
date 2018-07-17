using System.Collections.Generic;

namespace TicketTrader.Model
{
    public abstract class Payment
    {
        public string Name { get; set; }
        public Price ServiceFee { get; set; }
        public int Id { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Payment()
        {
            Orders = new List<Order>();
        }
    }
}