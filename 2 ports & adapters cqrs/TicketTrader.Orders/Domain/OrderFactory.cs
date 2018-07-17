using System;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.DDD;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
namespace TicketTrader.Orders.Domain
{
    class OrderFactory: AggregateFactory
    {
        public async Task<Order> CreateOrder(Id orderId, Id clientId)
        {
            return new Order(orderId, new Client(clientId), DateTime.Now, TimeSpan.FromMinutes(15));
        }
    }
}