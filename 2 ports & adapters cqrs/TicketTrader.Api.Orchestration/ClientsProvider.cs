using System;
using System.Net.Http;
using TicketTrader.EventDefinitions.Gateway.Client;
using TicketTrader.Payments.Gateway.Client;
// ReSharper disable ConvertToLambdaExpression

namespace TicketTrader.Api.Orchestration
{
    public class ClientsProvider: IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Lazy<EventDefinitionsClient> _eventDefinitionsClient;
        private readonly Lazy<PaymentsClient> _paymentsClient;

        public ClientsProvider(string eventsDefinitionsApiUrl, string paymentsApiUrl)
        {
            _httpClient = new HttpClient();

            _eventDefinitionsClient = new Lazy<EventDefinitionsClient>(() =>
            {
                return new EventDefinitionsClient(eventsDefinitionsApiUrl, _httpClient);
            });

            _paymentsClient = new Lazy<PaymentsClient>(() =>
            {
                return new PaymentsClient(paymentsApiUrl, _httpClient);
            });
        }

        public EventDefinitionsClient EventDefinitionsClient => _eventDefinitionsClient.Value;
        public PaymentsClient PaymentsClient => _paymentsClient.Value;

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
