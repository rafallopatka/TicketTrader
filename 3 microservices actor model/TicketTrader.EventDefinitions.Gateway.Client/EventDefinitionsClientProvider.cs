using System;
using System.Net.Http;

// ReSharper disable ConvertToLambdaExpression

namespace TicketTrader.EventDefinitions.Gateway.Client
{
    public class EventDefinitionsClientProvider: IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Lazy<EventDefinitionsClient> _eventDefinitionsClient;

        public EventDefinitionsClientProvider(string eventsDefinitionsApiGateway)
        {
            _httpClient = new HttpClient();

            _eventDefinitionsClient = new Lazy<EventDefinitionsClient>(() =>
            {
                return new EventDefinitionsClient(eventsDefinitionsApiGateway, _httpClient);
            });
        }

        public EventDefinitionsClient EventDefinitionsClient => _eventDefinitionsClient.Value;

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
