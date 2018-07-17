using System;
using System.Collections.Generic;

namespace TicketTrader.Model
{
    public class Order
    {
        public int Id { get; set; }
        public OrderState State { get; set; }

        public int? PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int? DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public SalesDocument SalesDocument { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public TimeSpan ExpirationTimeout { get; set; }

        public ICollection<TicketProduct> Tickets { get; set; }
        public ICollection<AdditionalProduct> AdditionalProducts { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}