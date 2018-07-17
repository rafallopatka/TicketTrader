using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.AspNetCore;
using Serilog;
using TicketTrader.Dal.Extensions;
using TicketTrader.Infrastructure.SharedNames;
using TicketTrader.Services.Extensions;


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

            var sqlConnectionString = Configuration.GetValue<string>(EnvironmentVariablesNames.DalDbConnectionString);
            services.AddPosgressDbContext(sqlConnectionString);

            services.AddApiServices();
            services.AddMvc();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            DalInitializer.InitializeDatabase(app, Configuration, env);

            var identityServerAddress =
            Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress);

            app.UseCors("default");

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = identityServerAddress,
                RequireHttpsMetadata = false,
                AllowedScopes = new[] {
                    TicketTraderScopes.ApiAdministrativeScope,
                    TicketTraderScopes.ApiSaleScope,
                    TicketTraderScopes.ApiInternalScope,
                    TicketTraderClaims.TicketTraderStoreClaims
                },
                ApiName = TicketTraderScopes.FullApiScope,
            });

            app.UseMvc();

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiSettings());
        }
    }
}
