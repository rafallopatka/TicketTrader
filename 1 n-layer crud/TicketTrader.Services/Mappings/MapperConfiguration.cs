using AutoMapper;

namespace TicketTrader.Services.Mappings
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
