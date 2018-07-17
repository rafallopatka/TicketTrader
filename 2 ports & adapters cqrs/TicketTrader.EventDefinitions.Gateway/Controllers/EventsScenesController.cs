using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketTrader.EventDefinitions.Services.Scenes.SceneDetails;

namespace TicketTrader.EventDefinitions.Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/Events/{eventId}/Scenes/")]
    public class EventsScenesController : Controller
    {
        private readonly ISceneDetailsProvider _sceneDetailsProvider;

        public EventsScenesController(ISceneDetailsProvider sceneDetailsProvider)
        {
            _sceneDetailsProvider = sceneDetailsProvider;
        }

        [HttpGet()]
        public async Task<IEnumerable<SceneDetailsDto>> Get(int eventId)
        {
            var scene = await _sceneDetailsProvider.GetSceneDetailsForEventAsync(eventId);

            return new[] { scene };
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
