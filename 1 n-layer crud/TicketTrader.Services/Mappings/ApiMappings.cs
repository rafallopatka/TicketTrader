using System.Linq;
using AutoMapper;
using Npgsql;
using TicketTrader.Model;
using TicketTrader.Services.Domain.Clients.Users;
using TicketTrader.Services.Domain.Deliveries;
using TicketTrader.Services.Domain.Events.EventList;
using TicketTrader.Services.Domain.Events.EventReservations;
using TicketTrader.Services.Domain.Orders.ClientsOrders;
using TicketTrader.Services.Domain.Orders.OrderDeliveries;
using TicketTrader.Services.Domain.Orders.OrderPayments;
using TicketTrader.Services.Domain.Orders.OrderTickets;
using TicketTrader.Services.Domain.Payments;
using TicketTrader.Services.Domain.PriceZones.PriceZonesList;
using TicketTrader.Services.Domain.Scenes.SceneDetails;

namespace TicketTrader.Services.Mappings
{
    public class ApiMappings : Profile
    {
        public ApiMappings()
        {
            CreateMap<Scene, SceneDetailsDto>();
            CreateMap<Event, EventListItemDto>()
                .ForMember(d => d.Description, m => m.MapFrom(s => s.Description.Description))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Description.Title))
                .ForMember(d => d.Authors, m => m.MapFrom(s => s.Description.Authors))
                .ForMember(d => d.Cast, m => m.MapFrom(s => s.Description.Cast))
                .ForMember(d => d.Categories, m => m.MapFrom(s => s.Description.EventCategories.Select(c => c.Category.Name).ToArray()));

            CreateMap<PriceOption, PriceZoneListItemDto.PriceOption>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.PriceId, m => m.MapFrom(s => s.Price.Id))
                .ForMember(d => d.GrossAmount, m => m.MapFrom(s => s.Price.GrossAmount))
                .ForMember(d => d.NetAmount, m => m.MapFrom(s => s.Price.NetAmount))
                .ForMember(d => d.VatRate, m => m.MapFrom(s => s.Price.VatRate));

            CreateMap<PriceZone, PriceZoneListItemDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Options, m => m.MapFrom(s => s.Options));

            CreateMap<IndividualClient, UserClientDto>()
                .ForMember(d => d.ClientId, m => m.MapFrom(s => s.Id));

            CreateMap<UserDto, IndividualClient>();

            CreateMap<OrderState, ClientOrderState>();
            CreateMap<ClientOrderState, OrderState>();

            CreateMap<Order, ClientOrderDto>();

            CreateMap<Reservation, SeatReservationDto>()
                .ForMember(d => d.SceneSeatId, m => m.MapFrom(s => s.Seat.SceneSeatId));

            CreateMap<TicketProduct, TicketOrderDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.PriceOptionId, m => m.MapFrom(s => s.PriceOption))

                .ForMember(d => d.PriceZoneId, m => m.MapFrom(s => s.PriceOption.PriceZoneId))
                .ForMember(d => d.PriceZoneName, m => m.MapFrom(s => s.PriceOption.PriceZone.Name))
                .ForMember(d => d.ClientId, m => m.MapFrom(s => s.ClientId))
                .ForMember(d => d.EventId, m => m.MapFrom(s => s.EventId))
                .ForMember(d => d.GrossAmount, m => m.MapFrom(s => s.PriceOption.Price.GrossAmount))
                .ForMember(d => d.PriceOptionId, m => m.MapFrom(s => s.PriceOption.Id))
                .ForMember(d => d.PriceOptionName, m => m.MapFrom(s => s.PriceOption.Name))
                .ForMember(d => d.SceneSeatIds, m => m.MapFrom(s => s.Reservations.Select(x => x.Seat.SceneSeatId)));


            CreateMap<Payment, PaymentTypeDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.PriceId, m => m.MapFrom(s => s.ServiceFee.Id))
                .ForMember(d => d.GrossAmount, m => m.MapFrom(s => s.ServiceFee.GrossAmount))
                .ForMember(d => d.NetAmount, m => m.MapFrom(s => s.ServiceFee.NetAmount))
                .ForMember(d => d.VatRate, m => m.MapFrom(s => s.ServiceFee.VatRate));

            CreateMap<Order, OrderPaymentDto>()
                .ForMember(d => d.OrderId, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.PaymentId, m => m.MapFrom(s => s.PaymentId));

            CreateMap<Delivery, DeliveryTypeDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.PriceId, m => m.MapFrom(s => s.ServiceFee.Id))
                .ForMember(d => d.GrossAmount, m => m.MapFrom(s => s.ServiceFee.GrossAmount))
                .ForMember(d => d.NetAmount, m => m.MapFrom(s => s.ServiceFee.NetAmount))
                .ForMember(d => d.VatRate, m => m.MapFrom(s => s.ServiceFee.VatRate));

            CreateMap<Order, OrderDeliveryDto>()
                .ForMember(d => d.OrderId, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.DeliveryId, m => m.MapFrom(s => s.DeliveryId));
        }
    }
}