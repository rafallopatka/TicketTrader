using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketTrader.Orders.ReadModel.Tickets;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;
using MongoDB.Driver;
using TicketTrader.Orders.Domain;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class TicketReadModelRepository: MongoReadSideRepository<OrderTicketReadModel>, ITicketFinder, IOrderTicketsDenormalizer
    {
        public TicketReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderTicketReadModel>> GetClientTicketForEventAsync(string clientId, string orderId, string eventId)
        {
            var result = await Collection.FindAsync(x => x.OrderId == orderId && x.EventId == eventId);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<OrderTicketReadModel>> GetClientTicketsAsync(string clientId, string orderId)
        {
            var result = await Collection.FindAsync(x => x.OrderId == orderId);
            return await result.ToListAsync();
        }

        public async Task CreateTicket(Order order, Ticket ticket)
        {
            var readModel = new OrderTicketReadModel
            {
                Id = ticket.Id.ToString(),
                EventId = ticket.EventId.ToString(),
                OrderId = order.Id.ToString(),
                ClientId = order.Client.Id.ToString(),
                SceneSeatIds = new List<string>(){ ticket.Seat.Id.ToString() },
                PriceOptionId = ticket.PriceOption.Id.ToString(),
                GrossAmount = ticket.PriceOption.GrossAmount,
                PriceOptionName = ticket.PriceOption.PriceOptionName,
                PriceZoneName = ticket.PriceOption.PriceZoneName
            };

            await Collection.InsertOneAsync(readModel);
        }

        public async Task RemoveTicket(Ticket ticket)
        {
            await Collection.DeleteOneAsync(x => x.Id == ticket.Id.ToString());
        }
    }
}
