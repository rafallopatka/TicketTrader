using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.OrderTickets
{
    public interface IOrderTicketsService
    {
        Task<TicketOrderDto> ReserveTicketAsync(int eventId, int clientId, int orderId, SeatPriceOptionDto option);
        Task<IEnumerable<TicketOrderDto>> GetClientTicketsForEventAsync(int eventId, int clientId, int orderId);
        Task<IEnumerable<TicketOrderDto>> GetClientTicketsAsync(int clientId, int orderId);
        Task RemoveTicketAsync(int eventId, int clientId, int orderId, int ticketId);
    }
}
