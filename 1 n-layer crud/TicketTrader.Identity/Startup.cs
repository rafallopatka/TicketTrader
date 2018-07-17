using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            var sqlConnectionString = Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerDbConnecionString);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)
                )
            );

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddOperationalStore(builder => builder.UseNpgsql(sqlConnectionString,
                    options => options.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationStore(builder => builder.UseNpgsql(sqlConnectionString,
                    options => options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<IdentityUser>();               

            var webClientAddress = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress);

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
            var webClientAddress = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderWebHostAddress);
            ApplicationDbInitializer.InitializeDatabase(app, Configuration, env);

            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");
            app.UseIdentity();
            app.UseIdentityServer();

            app.UseSecurityOptions(webClientAddress);
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
