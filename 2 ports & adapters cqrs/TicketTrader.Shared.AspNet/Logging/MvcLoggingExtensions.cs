using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TicketTrader.Shared.AspNet.Logging
{
    public static class MvcLoggingExtensions
    {
        public static void LogGlobalErrors(this IMvcBuilder mvc)
        {
            mvc.AddMvcOptions(o =>
            {
                o.Filters.Add(new GlobalExceptionFilter());
            });
        }

        public static void ConfigureLogging(this ILoggerFactory loggerFactory, 
            IConfiguration configuration, 
            IApplicationLifetime appLifetime, 
            string logTag)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            GlobalExceptionFilter.Init(loggerFactory, logTag);

            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
