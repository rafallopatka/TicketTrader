using System.Threading.Tasks;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Payments.ReadModel
{
    public interface IPaymentDenormalizer: IDenormalizer
    {
        Task AddPayment(Payment payment);
        Task UpdatePaymentAsync(Payment payment, PaymentStatus status);
    }
}
