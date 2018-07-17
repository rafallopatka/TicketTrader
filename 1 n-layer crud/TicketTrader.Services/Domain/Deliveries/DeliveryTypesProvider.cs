using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Deliveries
{
    public class DeliveryTypesProvider : IDeliveryTypesProvider
    {
        private readonly DalContext _dalContext;

        public DeliveryTypesProvider(DalContext dalContext)
        {
            _dalContext = dalContext;
        }

        public async Task<IList<DeliveryTypeDto>> GetDeliveryTypesAsync()
        {
            var data = await EntityFrameworkQueryableExtensions.Include<Delivery, Price>(_dalContext
                    .Deliveries, x => x.ServiceFee)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IList<DeliveryTypeDto>>();

        }
    }
}