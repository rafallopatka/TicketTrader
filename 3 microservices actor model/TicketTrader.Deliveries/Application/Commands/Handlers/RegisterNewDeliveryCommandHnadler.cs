using System.Threading.Tasks;
using TicketTrader.Deliveries.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Application.Commands.Handlers
{
    class RegisterNewDeliveryCommandHnadler: ICommandHandler<RegisterNewDeliveryCommand>
    {
        private readonly IRepository<Delivery> _deliveries;
        private readonly DeliveryFactory _factory;

        public RegisterNewDeliveryCommandHnadler(IRepository<Delivery> deliveries, 
            DeliveryFactory factory)
        {
            _deliveries = deliveries;
            _factory = factory;
        }

        public async Task HandleAsync(RegisterNewDeliveryCommand command)
        {
            var type = new DeliveryType(command.DeliveryTypeId);
            var order = new Order(command.OrderId);
            var address = new Address(command.Recipient, command.Address, command.City, command.PostalCode);

            var delivery = _factory.Create(type, order, address);
            await _deliveries.Save(delivery);
            await EventBus.Current.PublishAsync(new NewDeliveryRegisteredEvent(delivery.Id));
        }
    }
}
