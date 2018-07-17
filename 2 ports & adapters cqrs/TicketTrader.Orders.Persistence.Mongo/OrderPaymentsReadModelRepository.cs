using System;
using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Payments;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;
using MongoDB.Driver;

namespace TicketTrader.Orders.Persistence.Mongo
{
    class OrderPaymentsReadModelRepository : MongoReadSideRepository<OrderPaymentReadModel>, IOrderPaymentsFinder, IOrderPaymentDenormalizer
    {
        public OrderPaymentsReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<OrderPaymentReadModel> GetSelectedPaymentAsync(string clientId, string orderId)
        {
            var result = await Collection.FindAsync(model => model.Id == orderId);
            return await result.SingleOrDefaultAsync();
        }

        public async Task UpdatePaymentMethod(Order order)
        {
            var readModel = new OrderPaymentReadModel
            {
                Id = order.Id.ToString(),
                PaymentId = order.PaymentMethod.Option.ToString()
            };

            await Collection.ReplaceOneAsync(s => s.Id.Equals(order.Id.ToString()), readModel, new UpdateOptions
            {
                IsUpsert = true
            });
        }
    }
}