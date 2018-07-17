using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Shared;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class ClientOrderReadModelRepository: MongoReadSideRepository<ClientOrderReadModel>, IClientOrdersFinder, IClientOrdersDenormalizer
    {
        public ClientOrderReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClientOrderReadModel>> GetClientOrdersByStateAsync(string clientId, OrderStateReadModel orderStateReadModel)
        {
            var results = await Collection.FindAsync(model => model.ClientId == clientId && model.State == orderStateReadModel);
            return await results.ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            var readModel = new ClientOrderReadModel
            {
                Id = order.Id.ToString(),
                ClientId = order.Client.Id.ToString(),
                State = (OrderStateReadModel) order.OrderStatus,
                ExpirationTimeout = order.ExpirationTimeOut,
                CreateDateTime = order.CreateDateTime,
                UpdateDateTime = order.UpdateDateTime
            };

            await Collection.InsertOneAsync(readModel);
        }

        public async Task UpdateOrder(Order order)
        {
            var readModel = new ClientOrderReadModel
            {
                Id = order.Id.ToString(),
                ClientId = order.Client.Id.ToString(),
                State = (OrderStateReadModel)order.OrderStatus,
                ExpirationTimeout = order.ExpirationTimeOut,
                CreateDateTime = order.CreateDateTime,
                UpdateDateTime = order.UpdateDateTime
            };

            await Collection.ReplaceOneAsync(s => s.Id == order.Id.ToString(), readModel);
        }
    }
}
