using System;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketTrader.Identity.Data;
using TicketTrader.Identity.Extensions;
using TicketTrader.Identity.Services.Messaging;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Identity
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddMvc();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var sqlConnectionString = Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerDbConnecionString, "NoDB");
            var identityServerAddres = Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
                {
                    options.PublicOrigin = identityServerAddres;
                    options.IssuerUri = identityServerAddres;
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(sqlConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(sqlConnectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            var webClientAddress = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress, "WebClientAddress");

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(webClientAddress)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
             ApplicationDbInitializer.InitializeDatabase(app, Configuration, env);

            var webClientAddress = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress);

            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");
            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseSecurityOptions(webClientAddress);
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
