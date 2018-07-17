using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    [DataContract]
    public class CustomerRegisteredEvent: DomainEvent
    {
        [DataMember]
        public Id CustomerId { get; protected set; }
    }
}