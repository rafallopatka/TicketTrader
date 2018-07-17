using System.Threading.Tasks;


namespace TicketTrader.Shared.Base.DDD
{
    public interface IEventHandler<in TDomainEvent> where TDomainEvent: DomainEvent
    {
        Task Handle(TDomainEvent domainEvent);
    }
}
