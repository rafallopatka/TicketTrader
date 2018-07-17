
namespace TicketTrader.Shared.Base.DDD
{
    public abstract class DomainEvent
    {
        public Id EventId { get; }

        protected DomainEvent()
        {
            EventId = Id.New();
        }
    }
}