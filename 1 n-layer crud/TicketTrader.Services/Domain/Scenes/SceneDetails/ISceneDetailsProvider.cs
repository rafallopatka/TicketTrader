using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Scenes.SceneDetails
{
    public interface ISceneDetailsProvider
    {
        Task<SceneDetailsDto> GetSceneDetailsForEventAsync(int eventId);
    }
}