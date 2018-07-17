using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Payments
{
    public interface IOrderPaymentsFinder : IFinder
    {
        Task<OrderPaymentReadModel> GetSelectedPaymentAsync(string clientId, string orderId);
    }
}
