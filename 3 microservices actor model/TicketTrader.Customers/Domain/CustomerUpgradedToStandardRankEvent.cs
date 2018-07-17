using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    [DataContract]
    class CustomerUpgradedToStandardRankEvent : DomainEvent
    {
        protected CustomerUpgradedToStandardRankEvent()
        {
            
        }

        public CustomerUpgradedToStandardRankEvent(Id id)
        {
            CustomerId = id;
        }

        [DataMember]
        public Id CustomerId { get; protected set; }
    }
}