using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.AspNetCore;
using Serilog;
using TicketTrader.Api.Services.ClientsOrders;
using TicketTrader.Api.Services.Customers;
using TicketTrader.Api.Services.Delivery;
using TicketTrader.Api.Services.DeliveryTypes;
using TicketTrader.Api.Services.OrderDeliveries;
using TicketTrader.Api.Services.OrderPayments;
using TicketTrader.Api.Services.Orders;
using TicketTrader.Api.Services.OrderTickets;
using TicketTrader.Api.Services.Payment;
using TicketTrader.Api.Services.PaymentTypes;
using TicketTrader.Api.Services.Reservations;
using TicketTrader.EventDefinitions.Gateway.Client;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Api
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

        public void ConfigureServices(IServiceCollection services)
        {
            var webAppAddres = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress);
            var eventDefinitionsGateway = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderEventDefinitionsGateway);

            services.AddSingleton(provider => new EventDefinitionsClientProvider(eventDefinitionsGateway));
            services.AddSingleton<UserClientService>();
            services.AddSingleton<PaymentTypesProvider>();
            services.AddSingleton<PaymentService>();
            services.AddSingleton<DeliveryService>();
            services.AddSingleton<DeliveryTypesProvider>();
            services.AddSingleton<ClientOrdersProvider>();
            services.AddSingleton<ClientsOrderService>();
            services.AddSingleton<OrderDeliveriesService>();
            services.AddSingleton<OrderPaymentsService>();
            services.AddSingleton<OrderService>();
            services.AddSingleton<OrderTicketsService>();
            services.AddSingleton<ReservationsService>();

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddAuthorization(options => options.AddPolicyRules());
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(webAppAddres)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build();
                });
            });

            var identityServerAddress =
                Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress);

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = identityServerAddress;
                    opt.RequireHttpsMetadata = false;
                    opt.ApiName = TicketTraderScopes.FullApiScope;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            app.UseCors("default");

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiSettings());
        }
    }
}
