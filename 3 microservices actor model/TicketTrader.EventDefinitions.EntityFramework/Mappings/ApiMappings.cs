using System.Linq;
using AutoMapper;
using TicketTrader.EventDefinitions.Entities;
using TicketTrader.EventDefinitions.Services.EventsList;
using TicketTrader.EventDefinitions.Services.PriceZonesList;
using TicketTrader.EventDefinitions.Services.Scenes.SceneDetails;

namespace TicketTrader.EventDefinitions.EntityFramework.Mappings
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
        }
    }
}