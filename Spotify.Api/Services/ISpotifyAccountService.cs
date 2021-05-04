using System.Threading.Tasks;

namespace Spotify.Api.Services
{
    public interface ISpotifyAccountService
    {
        Task NewAccessToken();
        Task<string> GetAccessToken();
    }
}