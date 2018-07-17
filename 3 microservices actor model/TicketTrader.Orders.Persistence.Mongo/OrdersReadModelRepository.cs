using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Orders;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;
using MongoDB.Driver;
using TicketTrader.Orders.ReadModel.Shared;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class OrdersReadModelRepository: MongoReadSideRepository<OrderReadModel>, IOrderFinder, IOrdersDenormalizer
    {
        public OrdersReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderReadModel>> GetAwaitingOrders()
        {
            var result = await Collection.FindAsync(model => model.State == OrderStateReadModel.Commited);
            return await result.ToListAsync();
        }

        public async Task UpdateOrderStatus(Order order)
        {
            var readModel = new OrderReadModel
            {
                Id = order.Id.ToString(),
                State = (OrderStateReadModel) order.OrderStatus
            };

            await Collection.ReplaceOneAsync(model => model.Id == order.Id.ToString(), readModel);
        }

        public async Task CreateOrder(Order order)
        {
            var readModel = new OrderReadModel
            {
                Id = order.Id.ToString(),
                State = (OrderStateReadModel)order.OrderStatus
            };

            await Collection.InsertOneAsync(readModel);
        }

        public async Task DiscardOrder(Order order)
        {
            var readModel = new OrderReadModel
            {
                Id = order.Id.ToString(),
                State = (OrderStateReadModel)order.OrderStatus
            };

            await Collection.ReplaceOneAsync(model => model.Id == order.Id.ToString(), readModel);
        }
    }
}
