using System.Threading.Tasks;

namespace TicketTrader.EventDefinitions.Services.Scenes.SceneDetails
{
    public interface ISceneDetailsProvider
    {
        Task<SceneDetailsDto> GetSceneDetailsForEventAsync(int eventId);
    }
}