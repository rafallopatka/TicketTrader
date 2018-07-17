using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.CQRS.Commands
{
    public abstract class QueryBus
    {
        public abstract Task<TResponse> Query<TQuery, TResponse>(TQuery query)
            where TResponse : IQueryResponse
            where TQuery : IQuery;

        public abstract void Subscribe<TQuery, TResponse>(IQueryHandler<TQuery, TResponse> handler)
            where TResponse : IQueryResponse where TQuery : IQuery;

        public static QueryBus Current { get; set; }
    }
}