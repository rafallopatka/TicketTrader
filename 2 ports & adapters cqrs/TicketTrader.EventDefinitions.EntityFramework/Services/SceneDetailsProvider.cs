using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.EventDefinitions.Entities;
using TicketTrader.EventDefinitions.EntityFramework.Mappings;
using TicketTrader.EventDefinitions.Services.Scenes.SceneDetails;
using Z.EntityFramework.Plus;

namespace TicketTrader.EventDefinitions.EntityFramework.Services
{
    public class SceneDetailsProvider : ISceneDetailsProvider
    {
        private readonly DalContext _dalContext;

        public SceneDetailsProvider(DalContext dalContext)
        {
            _dalContext = dalContext;
        }

        public async Task<SceneDetailsDto> GetSceneDetailsForEventAsync(int eventId)
        {
            Scene scene = (await _dalContext
                    .Scenes
                    .AsNoTracking()
                    .FromCacheAsync())
                .Single(x => x.EventId == eventId);
                

            return scene.MapTo<SceneDetailsDto>();
        }
    }
}
