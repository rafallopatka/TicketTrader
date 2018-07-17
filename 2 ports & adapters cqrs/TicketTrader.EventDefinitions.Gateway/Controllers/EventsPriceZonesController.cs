using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketTrader.EventDefinitions.Services.PriceZonesList;

namespace TicketTrader.EventDefinitions.Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/Events/{eventId}/PriceZones")]
    public class EventsPriceZonesController : Controller
    {
        private readonly IPriceZoneListProvider _priceZones;

        public EventsPriceZonesController(IPriceZoneListProvider priceZones)
        {
            _priceZones = priceZones;
        }

        [HttpGet]
        public async Task<IEnumerable<PriceZoneListItemDto>> Get(int eventId)
        {
            return await _priceZones.GetEventPriceZonesAsync(eventId);
        }

        [HttpGet("{id}")]
        public string Get(int eventId, int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Post(int eventId, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put(int eventId, int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int eventId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
