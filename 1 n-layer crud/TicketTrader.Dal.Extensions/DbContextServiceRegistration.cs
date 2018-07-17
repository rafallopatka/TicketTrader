using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Dal.Extensions
{
    public static class DbContextServiceRegistration
    {
        public static void AddPosgressDbContext(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DalContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<DalContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly))
            );
        }
    }
}
