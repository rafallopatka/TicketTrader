namespace TicketTrader.Shared.Base.DDD
{
    public abstract class AggregateFactory
    {
        protected EventBus EventBus => EventBus.Current;
    }
}