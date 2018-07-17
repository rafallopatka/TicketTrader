using System.Threading.Tasks;
using MongoDB.Driver;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Deliveries;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class OrderDeliveriesReadModelRepository : MongoReadSideRepository<OrderDeliveryReadModel>, IOrderDeliveriesFinder, IOrderDeliveriesDenormalizer
    {
        public OrderDeliveriesReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<OrderDeliveryReadModel> GetSelectedDeliveryAsync(string clientId, string orderId)
        {
            var result = await Collection.FindAsync(model => model.Id == orderId);
            return await result.SingleOrDefaultAsync();
        }

        public async Task UpdateDeliveryMethod(Order order)
        {
            var readModel = new OrderDeliveryReadModel
            {
                Id = order.Id.ToString(),
                DeliveryId = order.DeliveryMethod.Option.ToString()
            };

            await Collection.ReplaceOneAsync(s => s.Id.Equals(order.Id.ToString()), readModel, new UpdateOptions
            {
                IsUpsert = true
            });
        }
    }
}