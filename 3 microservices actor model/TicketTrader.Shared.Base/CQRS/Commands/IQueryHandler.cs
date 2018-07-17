using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.CQRS.Commands
{
    public interface IQueryHandler<in TQuery, TResponse> 
        where TQuery : IQuery
        where TResponse : IQueryResponse
    {
        Task<TResponse> Handle(TQuery query);
    }
}