using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Payments.ReadModel
{
    public interface IPaymentsFinder: IFinder
    {
        Task<IEnumerable<PaymentReadModel>> GetWaitingPaymentsAsync();
        Task<IEnumerable<PaymentTypeReadModel>> GetPaymentTypesAsync();
        Task<PaymentReadModel> GetPaymentForOrderAsync(string orderId);
    }
}
