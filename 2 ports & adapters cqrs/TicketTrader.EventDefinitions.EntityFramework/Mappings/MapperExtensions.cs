using AutoMapper;

namespace TicketTrader.EventDefinitions.EntityFramework.Mappings
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object @this)
        {
            return Mapper.Map<TDestination>(@this);
        }
    }
}