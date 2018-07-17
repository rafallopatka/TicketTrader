using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Deliveries.ReadModel;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;
using DeliveryType = TicketTrader.Deliveries.ReadModel.DeliveryType;

namespace TicketTrader.Deliveries.Persistence.Mongo
{
    class DeliveriesReadSideRepository : MongoReadSideRepository<DeliveryReadModel>, IDeliveryFinder, IDeliveryDenormalizer
    {
        public DeliveriesReadSideRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync()
        {
            return new[]
            {
                new DeliveryType
                {
                    DeliveryTypeId = "1",
                    GrossAmount = 10,
                    VatRate = 0,
                    NetAmount = 10,
                    Name = "Post",
                    PriceId = 1
                },
                new DeliveryType
                {
                    DeliveryTypeId = "2",
                    GrossAmount = 15,
                    VatRate = 0,
                    NetAmount = 15,
                    Name = "Courier",
                    PriceId = 2
                },
                new DeliveryType
                {
                    DeliveryTypeId = "3",
                    GrossAmount = 0,
                    VatRate = 0,
                    NetAmount = 0,
                    Name = "Online",
                    PriceId = 3
                },
            };
        }

        public async Task<IEnumerable<DeliveryReadModel>> GetWaitingDeliveriesAsync()
        {
            var result = await Collection.FindAsync(model => model.Status == DeliveryReadModel.DeliveryStatus.Awaiting);
            return await result.ToListAsync();
        }

        public async Task AddDelivery(Delivery delivery)
        {
            var model = new DeliveryReadModel
            {
                OrderId = delivery.Order.Id.ToString(),
                Id = delivery.Id.ToString(),
                DeliveryTypeId = delivery.Type.Id.ToString()
            };

            switch (delivery.DeliveryStatus)
            {
                case DeliveryStatus.New:
                    model.Status = DeliveryReadModel.DeliveryStatus.Awaiting;
                    break;
                case DeliveryStatus.Completed:
                    model.Status = DeliveryReadModel.DeliveryStatus.Completed;
                    break;
                case DeliveryStatus.Failed:
                    model.Status = DeliveryReadModel.DeliveryStatus.Failed;
                    break;
                case DeliveryStatus.Canceled:
                    model.Status = DeliveryReadModel.DeliveryStatus.Canceled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await Collection.InsertOneAsync(model);
        }

        public async Task UpdateDelivery(Delivery delivery)
        {
            var readModel = new DeliveryReadModel
            {
                OrderId = delivery.Order.Id.ToString(),
                Id = delivery.Id.ToString(),
                DeliveryTypeId = delivery.Type.Id.ToString()
            };

            switch (delivery.DeliveryStatus)
            {
                case DeliveryStatus.New:
                    readModel.Status = DeliveryReadModel.DeliveryStatus.Awaiting;
                    break;
                case DeliveryStatus.Completed:
                    readModel.Status = DeliveryReadModel.DeliveryStatus.Completed;
                    break;
                case DeliveryStatus.Failed:
                    readModel.Status = DeliveryReadModel.DeliveryStatus.Failed;
                    break;
                case DeliveryStatus.Canceled:
                    readModel.Status = DeliveryReadModel.DeliveryStatus.Canceled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await Collection.ReplaceOneAsync(model => model.Id == delivery.Id.ToString(), readModel);
        }
    }
}
