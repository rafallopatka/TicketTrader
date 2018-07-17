using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Scenes.SceneDetails
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
            Scene scene = await _dalContext
                .Scenes
                .AsNoTracking()
                .SingleAsync(x => x.EventId == eventId);

            return scene.MapTo<SceneDetailsDto>();
        }
    }
}
