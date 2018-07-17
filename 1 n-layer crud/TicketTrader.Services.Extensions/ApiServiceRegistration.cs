using Microsoft.Extensions.DependencyInjection;
using TicketTrader.Services.Core;
using TicketTrader.Services.Domain.Clients.Users;
using TicketTrader.Services.Domain.Deliveries;
using TicketTrader.Services.Domain.Events.EventList;
using TicketTrader.Services.Domain.Orders.ClientsOrders;
using TicketTrader.Services.Domain.Orders.OrderDeliveries;
using TicketTrader.Services.Domain.Orders.OrderPayments;
using TicketTrader.Services.Domain.Orders.OrdersManagement;
using TicketTrader.Services.Domain.Orders.OrderTickets;
using TicketTrader.Services.Domain.Payments;
using TicketTrader.Services.Domain.PriceZones.PriceZonesList;
using TicketTrader.Services.Domain.Reservations;
using TicketTrader.Services.Domain.Scenes.SceneDetails;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Extensions
{
    public static class ApiServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            ApiMapperConfiguration.Configure();

            services.AddTransient<ICurrentTimeProvider, CurrentTimeProvider>();

            services.AddTransient<IEventListProvider, EventListProvider>();
            services.AddTransient<ISceneDetailsProvider, SceneDetailsProvider>();
            services.AddTransient<IPriceZoneListProvider, PriceZoneListProvider>();
            services.AddTransient<IReservationsService, ReservationsService>();

            services.AddTransient<IUserClientCrudService, UserClientCrudService>();
            services.AddTransient<IClientsOrdersProvider, ClientOrdersProvider>();
            services.AddTransient<IOrderTicketsService, OrderTicketsService>();
            services.AddTransient<IClientsOrderService, ClientsOrderService>();

            services.AddTransient<IPaymentTypesProvider, PaymentTypesProvider>();
            services.AddTransient<IOrderPaymentsService, OrderPaymentsService>();
            services.AddTransient<IDeliveryTypesProvider, DeliveryTypesProvider>();
            services.AddTransient<IOrderDeliveriesService, OrderDeliveriesService>();

            services.AddTransient<IOrderManagementService, OrderManagementService>();
        }
    }
}
