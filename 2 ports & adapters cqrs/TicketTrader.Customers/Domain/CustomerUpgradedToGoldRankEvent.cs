using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    [DataContract]
    class CustomerUpgradedToGoldRankEvent : DomainEvent
    {
        [DataMember]
        public Id CustomerId { get; protected set; }
    }
}