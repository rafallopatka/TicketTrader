using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientTicketsQueryHandler : IQueryHandler<GetClientTicketsQuery, GetClientTicketsQuery.Response>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public GetClientTicketsQueryHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task<GetClientTicketsQuery.Response> Handle(GetClientTicketsQuery query)
        {
            var response = await _domainActorSystem.Query<RespondCustomerTickets>(new GetCustomerTickets(query.ClientId, query.OrderId));

            return new GetClientTicketsQuery.Response
            {
                Tickets = response.Tickets.Select(x => new GetClientTicketsQuery.OrderTickets
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ClientId = x.ClientId,
                    SceneSeatIds = x.SceneSeatIds.ToList(),
                    EventId = x.EventId,
                    PriceOptionId = x.PriceOptionId,
                    GrossAmount = x.GrossAmount,
                    PriceOptionName = x.PriceOptionName,
                    PriceZoneName = x.PriceZoneName
                }).ToList()
            };
        }
    }
}