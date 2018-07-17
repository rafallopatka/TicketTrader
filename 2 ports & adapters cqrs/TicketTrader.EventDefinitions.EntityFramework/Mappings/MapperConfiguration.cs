using AutoMapper;

namespace TicketTrader.EventDefinitions.EntityFramework.Mappings
{
    public static class ApiMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ApiMappings>();
                cfg.CreateMissingTypeMaps = true;
            });
        }
    }
}
