using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Payments
{
    public interface IPaymentTypesProvider
    {
        Task<IList<PaymentTypeDto>> GetPaymentTypes();
    }
}
