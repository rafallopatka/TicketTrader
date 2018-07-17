using System.Threading.Tasks;
using RawRabbit;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.Bus.RabbitMq
{
    public class RabbitMqQueryBus : QueryBus
    {
        public override Task<TResponse> Query<TQuery, TResponse>(TQuery query)
        {
            return Bus.Client.RequestAsync<TQuery, TResponse>(query);
        }

        public override void Subscribe<TQuery, TResponse>(IQueryHandler<TQuery, TResponse> handler)
        {
            Bus.Client.RespondAsync<TQuery, TResponse>(async query => await handler.Handle(query));
        }
    }
}
