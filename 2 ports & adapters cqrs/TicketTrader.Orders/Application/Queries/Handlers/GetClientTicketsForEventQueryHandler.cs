using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Tickets;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetClientTicketsForEventQueryHandler : IQueryHandler<GetClientTicketsForEventQuery, GetClientTicketsForEventQuery.Response>
    {
        private readonly ITicketFinder _ticketFinder;

        public GetClientTicketsForEventQueryHandler(ITicketFinder ticketFinder)
        {
            _ticketFinder = ticketFinder;
        }

        public async Task<GetClientTicketsForEventQuery.Response> Handle(GetClientTicketsForEventQuery query)
        {
            var response =
                await _ticketFinder.GetClientTicketForEventAsync(query.ClientId, query.OrderId, query.EventId);

            return new GetClientTicketsForEventQuery.Response
            {
                Tickets = response.Select(x => new GetClientTicketsForEventQuery.ClientTicketsForOrder
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ClientId = x.ClientId,
                    EventId = x.EventId,
                    PriceOptionId = x.PriceOptionId,
                    SceneSeatIds = x.SceneSeatIds,
                    GrossAmount = x.GrossAmount,
                    PriceOptionName = x.PriceOptionName,
                    PriceZoneName = x.PriceZoneName
                }).ToList()
            };
        }
    }
}