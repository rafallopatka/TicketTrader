using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientTicketsForEventQueryHandler : IQueryHandler<GetClientTicketsForEventQuery, GetClientTicketsForEventQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetClientTicketsForEventQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task<GetClientTicketsForEventQuery.Response> Handle(GetClientTicketsForEventQuery query)
        {
            var response = await _domainActorSystem.Query<RespondCustomerTicketsForEvent>(new GetCustomerTicketsForEvent(query.ClientId, query.OrderId, query.EventId));

            return new GetClientTicketsForEventQuery.Response
            {
                Tickets = response.Tickets.Select(x => new GetClientTicketsForEventQuery.ClientTicketsForOrder
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ClientId = x.ClientId,
                    EventId = x.EventId,
                    PriceOptionId = x.PriceOptionId,
                    SceneSeatIds = x.SceneSeatIds.ToList(),
                    GrossAmount = x.GrossAmount,
                    PriceOptionName = x.PriceOptionName,
                    PriceZoneName = x.PriceZoneName
                }).ToList()
            };
        }
    }
}