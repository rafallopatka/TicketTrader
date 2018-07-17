using System.Reflection;

namespace TicketTrader.Shared.Bus.RabbitMq
{
    public static class ExchangeHelper
    {
        public static string GetName<T>(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
            }
            else
            {
                return $"{name}/{typeof(T).Name}";
            }
        }
    }
}
