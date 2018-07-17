using AutoMapper;

namespace TicketTrader.Services.Mappings
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object @this)
        {
            return Mapper.Map<TDestination>(@this);
        }
    }
}