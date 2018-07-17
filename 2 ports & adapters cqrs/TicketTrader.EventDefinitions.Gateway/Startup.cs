using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketTrader.EventDefinitions.EntityFramework;
using TicketTrader.Infrastructure.SharedNames;
using TicketTrader.Shared.AspNet;
using TicketTrader.Shared.AspNet.Logging;

namespace TicketTrader.EventDefinitions.Gateway
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetValue<string>(EnvironmentVariablesNames.DalDbConnectionString);
            services.AddEventDefinitionsEntityFrameworkServices(sqlConnectionString);

            services.AddMvc().LogGlobalErrors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            IApplicationLifetime appLifetime)
        {
            loggerFactory.ConfigureLogging(Configuration, appLifetime, "EventDefinitions.Gateway");
            DalInitializer.InitializeDatabase(app, Configuration, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
