using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketTrader.Payments.Domain;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;

namespace TicketTrader.Payments.Persistence.Mongo
{
    class PaymentsReadModelRepository : MongoReadSideRepository<PaymentReadModel>, IPaymentsFinder, IPaymentDenormalizer
    {
        public PaymentsReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentReadModel>> GetWaitingPaymentsAsync()
        {
            var result = await Collection.FindAsync(model => model.Status == PaymentReadModel.PaymentStatus.Awaiting);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<PaymentTypeReadModel>> GetPaymentTypesAsync()
        {
            return await Task.Run(() => new List<PaymentTypeReadModel>()
            {
                new PaymentTypeReadModel { Name = "Cash", GrossAmount = 1, NetAmount = 1, VatRate = 0, PaymentTypeId = "1" },
                new PaymentTypeReadModel { Name = "Card", GrossAmount = 2, NetAmount = 2, VatRate = 0, PaymentTypeId = "2" },
                new PaymentTypeReadModel { Name = "Online", GrossAmount = 3, NetAmount = 3, VatRate = 0, PaymentTypeId = "3" },
            });
        }

        public async Task<PaymentReadModel> GetPaymentForOrderAsync(string orderId)
        {
            var result = await Collection.FindAsync(model => model.OrderId == orderId);
            return await result.SingleOrDefaultAsync();
        }

        public async Task AddPayment(Payment payment)
        {
            var readModel = new PaymentReadModel
            {
                Id = payment.Id.ToString(),
                OrderId = payment.Order.Id.ToString(),
                Status = PaymentReadModel.PaymentStatus.Awaiting
            };

            await Collection.InsertOneAsync(readModel);
        }

        public async Task UpdatePaymentAsync(Payment payment, PaymentStatus status)
        {
            var readModel = new PaymentReadModel
            {
                Id = payment.Id.ToString(),
                OrderId = payment.Order.Id.ToString(),
            };

            switch (status)
            {
                case PaymentStatus.Started:
                    readModel.Status = PaymentReadModel.PaymentStatus.Awaiting;
                    break;
                case PaymentStatus.Successed:
                    readModel.Status = PaymentReadModel.PaymentStatus.Completed;
                    break;
                case PaymentStatus.Failed:
                    readModel.Status = PaymentReadModel.PaymentStatus.Failed;
                    break;
            }

            await Collection.ReplaceOneAsync(model => model.Id == payment.Id.ToString(), readModel);
        }
    }
}
