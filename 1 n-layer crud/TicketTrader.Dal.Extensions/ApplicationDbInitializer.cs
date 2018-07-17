using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Dal.Extensions
{
    public static class DalInitializer
    {
        public static void InitializeDatabase(IApplicationBuilder app, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                InitializeConfigurationDbContext(scope, configuration, env);
            }
        }

        private static void InitializeConfigurationDbContext(IServiceScope scope, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            var context = scope.ServiceProvider.GetRequiredService<DalContext>();
            //if (env.IsDevelopment())
            //    context.Database.EnsureDeleted();
            if (env.IsDevelopment())
                context.Database.Migrate();
            //TestDataSeed.FillContext(context);
        }
    }
}
