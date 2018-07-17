using System.Threading.Tasks;
using TicketTrader.Deliveries.Canonical.Commands;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Application.Commands.Handlers
{
    class RegisterDeliveryFailureCommandHandler: ICommandHandler<RegisterDeliveryFailureCommand>
    {
        private readonly IRepository<Delivery> _deliveries;

        public RegisterDeliveryFailureCommandHandler(IRepository<Delivery> deliveries)
        {
            _deliveries = deliveries;
        }

        public async Task HandleAsync(RegisterDeliveryFailureCommand command)
        {
            var delivery = await _deliveries.Get(Id.From(command.DeliveryId));
            delivery.MarkAsFailed();
            await _deliveries.Save(delivery);
            await EventBus.Current.PublishAsync(new DeliveryFailedEvent(delivery.Id));
        }
    }
}
