using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketTrader.Api.Client;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Management
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddHangfire(c =>
            {
                c.UseMemoryStorage(new MemoryStorageOptions());
            });

            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();

                return new HttpClientConfig
                {
                    ClientId = TicketTraderClients.ManagementClient,
                    IdentityServerAddress = config.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress),
                    ClientSecret = config.GetValue<string>(EnvironmentVariablesNames.TicketTraderApiSecret),
                    Scopes = new[] { TicketTraderScopes.ApiSaleScope, TicketTraderScopes.ApiInternalScope, TicketTraderScopes.ApiAdministrativeScope }
                };
            });

            services.AddSingleton<HttpClientFactory>();
            services.AddLogging();

            services.AddSingleton<PayJob>();
            services.AddSingleton<DiscardJob>();
            services.AddSingleton<DeliveryJob>();

            services.AddSingleton<Scheduler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime,
            IServiceProvider provider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            var scheduler = provider.GetService<Scheduler>();
            scheduler.Begin();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] {new DashboardAuthorizationFilter()}
            });

            app.UseHangfireServer();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("management");
            });
        }
    }
}
