using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Tickets;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientTicketsQueryHandler : IQueryHandler<GetClientTicketsQuery, GetClientTicketsQuery.Response>
    {
        private readonly ITicketFinder _finder;

        public GetClientTicketsQueryHandler(ITicketFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetClientTicketsQuery.Response> Handle(GetClientTicketsQuery query)
        {
            var response = await _finder.GetClientTicketsAsync(query.ClientId, query.OrderId);

            return new GetClientTicketsQuery.Response
            {
                Tickets = response.Select(x => new GetClientTicketsQuery.OrderTickets
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ClientId = x.ClientId,
                    SceneSeatIds = x.SceneSeatIds,
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