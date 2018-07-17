using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Orders.ReadModel.Payments
{
    public interface IOrderPaymentDenormalizer : IDenormalizer
    {
        Task UpdatePaymentMethod(Order order);
    }
}