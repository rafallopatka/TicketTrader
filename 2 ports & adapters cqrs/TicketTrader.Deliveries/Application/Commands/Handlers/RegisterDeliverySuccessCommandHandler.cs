using System.Threading.Tasks;
using TicketTrader.Deliveries.Canonical.Commands;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Application.Commands.Handlers
{
    class RegisterDeliverySuccessCommandHandler: ICommandHandler<RegisterDeliverySuccessCommand>
    {
        private readonly IRepository<Delivery> _deliveries;

        public RegisterDeliverySuccessCommandHandler(IRepository<Delivery> deliveries)
        {
            _deliveries = deliveries;
        }

        public async Task HandleAsync(RegisterDeliverySuccessCommand command)
        {
            var delivery = await _deliveries.Get(Id.From(command.DeliveryId));
            delivery.MarkAsCompleted();
            await _deliveries.Save(delivery);
            await EventBus.Current.PublishAsync(new DeliveryCompletedEvent(delivery.Id));
        }
    }
}
