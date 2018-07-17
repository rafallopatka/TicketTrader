using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketTrader.EventDefinitions.EntityFramework.Mappings;
using TicketTrader.EventDefinitions.EntityFramework.Services;
using TicketTrader.EventDefinitions.Services.EventsList;
using TicketTrader.EventDefinitions.Services.PriceZonesList;
using TicketTrader.EventDefinitions.Services.Scenes.SceneDetails;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.EventDefinitions.EntityFramework
{
    public static class EventDefinitionsEntityFrameworkRegistration
    {
        public static void AddEventDefinitionsEntityFrameworkServices(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DalContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<DalContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly))
            );

            ApiMapperConfiguration.Configure();

            services.AddTransient<ICurrentDateTimeProvider, CurrentDateTimeProvider>();
            services.AddTransient<IEventListProvider, EventListProvider>();
            services.AddTransient<ISceneDetailsProvider, SceneDetailsProvider>();
            services.AddTransient<IPriceZoneListProvider, PriceZoneListProvider>();
        }
    }
}
