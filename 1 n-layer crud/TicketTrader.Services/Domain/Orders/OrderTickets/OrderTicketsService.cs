using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Core;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Orders.OrderTickets
{
    public class OrderTicketsService : IOrderTicketsService
    {
        private readonly DalContext _context;

        public OrderTicketsService(DalContext context)
        {
            _context = context;
        }

        public async Task<TicketOrderDto> ReserveTicketAsync(int eventId, int clientId, int orderId, SeatPriceOptionDto option)
        {
            var priceOption = await _context
                .PriceOptions
                .Where(x => x.Id == option.PriceOptionId)
                .Include(x => x.PriceZone)
                .ThenInclude(x => x.Seats)
                .Include(x => x.Price)
                .Select(x => x)
                .SingleAsync();

            var seats = await _context
                .Seats
                .Where(x => x.SceneSeatId == option.SceneSeatId)
                .Where(x => x.EventId == eventId)
                .Select(x => x.Id)
                .ToListAsync();

            var reservations = await _context
                .Reservations
                .Where(x => x.OrderId == orderId)
                .Where(x => x.EventId == eventId)
                .Where(x => x.ClientId == clientId)
                .Where(x => seats.Any(seatId => x.SeatId == seatId))
                .Where(x => x.Discarded == false)
                .Where(x => x.Ticket == null)
                .ToListAsync();

            var ticket = new TicketProduct
            {
                EventId = eventId,
                ClientId = clientId,
                OrderId = orderId,
                PriceOption = priceOption,
            };
            ticket.Reservations.AddRange(reservations);

            _context.TicketProducts.Add(ticket);

            await _context.SaveChangesAsync();

            return ticket.MapTo<TicketOrderDto>();
        }

        public async Task<IEnumerable<TicketOrderDto>> GetClientTicketsForEventAsync(int eventId, int clientId, int orderId)
        {
            var data = await _context
                .TicketProducts
                .Where(x => x.EventId == eventId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.OrderId == orderId)
                .Include(x => x.PriceOption)
                .ThenInclude(x => x.PriceZone)
                .Include(x => x.PriceOption)
                .ThenInclude(x => x.Price)
                .Include(x => x.Reservations)
                .ThenInclude(x => x.Seat)
                .Select(x => x)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IEnumerable<TicketOrderDto>>();
        }

        public async Task<IEnumerable<TicketOrderDto>> GetClientTicketsAsync(int clientId, int orderId)
        {
            var data = await _context
                .TicketProducts
                .Where(x => x.ClientId == clientId)
                .Where(x => x.OrderId == orderId)
                .Include(x => x.PriceOption)
                .ThenInclude(x => x.PriceZone)
                .Include(x => x.PriceOption)
                .ThenInclude(x => x.Price)
                .Include(x => x.Reservations)
                .ThenInclude(x => x.Seat)
                .Select(x => x)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IEnumerable<TicketOrderDto>>();
        }

        public async Task RemoveTicketAsync(int eventId, int clientId, int orderId, int ticketId)
        {
            var reservations = await _context
                .Reservations
                .Where(x => x.EventId == eventId)
                .Where(x => x.OrderId == orderId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.TicketId == ticketId)
                .ToListAsync();

            reservations.ForEach(x =>
            {
                x.Discarded = true;
                x.TicketId = null;
            });

            var ticket = await _context
                .TicketProducts
                .Where(x => x.EventId == eventId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.OrderId == orderId)
                .Select(x => x)
                .SingleAsync(x => x.Id == ticketId);
      
            _context
                .TicketProducts
                .Remove(ticket);

            await _context.SaveChangesAsync();
        }
    }
}