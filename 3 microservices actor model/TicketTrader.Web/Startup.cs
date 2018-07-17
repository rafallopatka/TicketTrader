using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketTrader.Api.Client;
using TicketTrader.Infrastructure.SharedNames;

namespace TicketTrader.Web
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();

                return new HttpClientConfig
                {
                    ClientId = TicketTraderClients.InternalClient,
                    IdentityServerAddress = config.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress),
                    ClientSecret = config.GetValue<string>(EnvironmentVariablesNames.TicketTraderApiSecret),
                    Scopes = new[] { TicketTraderScopes.ApiSaleScope, TicketTraderScopes.ApiInternalScope }
                };
            });

            services.AddSingleton<HttpClientFactory>();
            services.AddLogging();

            services.AddSession();
            var identityServerAddress =
                Configuration.GetValue<string>(EnvironmentVariablesNames.IdentityServerHostAddress);

            var apiSecret = Configuration.GetValue<string>(EnvironmentVariablesNames.TicketTraderApiSecret);

            services.AddAuthentication("Cookies")
                .AddCookie()
                .AddOpenIdConnect("oidc", cfg =>
                {
                    cfg.SignInScheme = "Cookies";

                    cfg.Authority = identityServerAddress;
                    cfg.RequireHttpsMetadata = false;

                    cfg.ClientId = TicketTraderClients.WebClientId;
                    cfg.ClientSecret = apiSecret;

                    cfg.ResponseType = "code id_token";
                    cfg.Scope.Add(TicketTraderScopes.ApiSaleScope);
                    cfg.Scope.Add("offline_access");

                    cfg.GetClaimsFromUserInfoEndpoint = true;
                    cfg.SaveTokens = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
            app.UseStaticFiles();

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new List<string>() { "index.html" },
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "spa/store/dist/")),
                RequestPath = "/Spa/Store"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "spa/store/dist/")),
                RequestPath = "/Spa/Store"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "spa/store/dist/")),
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
