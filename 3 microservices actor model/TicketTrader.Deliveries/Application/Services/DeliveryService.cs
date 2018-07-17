using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketTrader.Deliveries.Application.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Kernel;

namespace TicketTrader.Deliveries.Application.Services
{
    public class DeliveryService : IEventHandler<NewOrderCommitedEvent>
    {
        public async Task Handle(NewOrderCommitedEvent domainEvent)
        {
            await CommandBus.Current.DispatchAsync(new RegisterNewDeliveryCommand
            {
                OrderId = domainEvent.OrderId.ToString(),
                DeliveryTypeId = domainEvent.PaymentMethodId.ToString()
            });
        }
    }
}
