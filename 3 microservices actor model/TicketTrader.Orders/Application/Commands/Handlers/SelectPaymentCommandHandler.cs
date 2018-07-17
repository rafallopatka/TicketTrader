using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class SelectPaymentCommandHandler: ICommandHandler<SelectPaymentCommand>
    {
        private readonly DomainActorSystem _domainActorSystem;

        public SelectPaymentCommandHandler(DomainActorSystem domainActorSystem)
        {
            _domainActorSystem = domainActorSystem;
        }

        public async Task HandleAsync(SelectPaymentCommand command)
        {
            _domainActorSystem.Execute(new SetPayment(command.ClientId, command.OrderId, command.PaymentTypeId));
        }
    }
}