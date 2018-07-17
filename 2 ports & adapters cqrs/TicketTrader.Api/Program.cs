using System.IO;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Bus.RabbitMq;

namespace TicketTrader.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            App.Current
                .UseRabbitMqBus()
                .Build()
                .RunSilently();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
