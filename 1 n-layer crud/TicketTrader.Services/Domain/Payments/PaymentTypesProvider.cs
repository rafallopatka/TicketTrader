using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Payments
{
    public class PaymentTypesProvider : IPaymentTypesProvider
    {
        private readonly DalContext _dalContext;

        public PaymentTypesProvider(DalContext dalContext)
        {
            _dalContext = dalContext;
        }

        public async Task<IList<PaymentTypeDto>> GetPaymentTypes()
        {
            var data = await _dalContext.Payments
                .Include(x => x.ServiceFee)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IList<PaymentTypeDto>>();
        }
    }
}