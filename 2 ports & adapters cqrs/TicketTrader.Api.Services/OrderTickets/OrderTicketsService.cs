using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.OrderTickets
{
    public class OrderTicketsService
    {
        public async Task<IEnumerable<TicketOrderDto>> GetClientTicketsForEventAsync(string eventId, string clientId, string orderId)
        {
            var query = new GetClientTicketsForEventQuery
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId
            };

            var response = await QueryBus.Current.Query<GetClientTicketsForEventQuery, GetClientTicketsForEventQuery.Response>(query);

            return response.Tickets.Select(x => new TicketOrderDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ClientId = x.ClientId,
                SceneSeatIds = x.SceneSeatIds,
                PriceOptionId = x.PriceOptionId,
                EventId = x.EventId,
                GrossAmount = x.GrossAmount,
                PriceOptionName = x.PriceOptionName,
                PriceZoneName = x.PriceZoneName
            });
        }

        public async Task<IEnumerable<TicketOrderDto>> GetClientTicketsAsync(string clientId, string orderId)
        {
            var query = new GetClientTicketsQuery
            {
                ClientId = clientId,
                OrderId = orderId
            };

            var data = await QueryBus.Current.Query<GetClientTicketsQuery, GetClientTicketsQuery.Response>(query);

            return data.Tickets.Select(x => new TicketOrderDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ClientId = x.ClientId,
                SceneSeatIds = x.SceneSeatIds,
                PriceOptionId = x.PriceOptionId,
                EventId = x.EventId,
                GrossAmount = x.GrossAmount,
                PriceOptionName = x.PriceOptionName,
                PriceZoneName = x.PriceZoneName
            });
        }

        public async Task<TicketOrderDto> ReserveTicketAsync(string eventId, string clientId, string orderId, SeatPriceOptionDto option)
        {
            var command = new ReserveTicketCommand
            {
                TicketId = Guid.NewGuid().ToString(),
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                PriceOptionId = option.PriceOptionId,
                SceneSeatId = option.SceneSeatId,
                GrossAmount = option.GrossAmount,
                PriceOptionName = option.PriceOptionName,
                PriceZoneName = option.PriceZoneName
            };

            await CommandBus.Current.DispatchAsync(command);

            return new TicketOrderDto
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                PriceOptionId = option.PriceOptionId,
                SceneSeatIds = new List<string>() {option.SceneSeatId},
                Id = command.TicketId,
                GrossAmount = option.GrossAmount,
                PriceOptionName = option.PriceOptionName,
                PriceZoneName = option.PriceZoneName
            };
        }

        public async Task RemoveTicketAsync(string eventId, string clientId, string orderId, string ticketId)
        {
            var command = new RemoveTicketCommand
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                TicketId = ticketId
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}