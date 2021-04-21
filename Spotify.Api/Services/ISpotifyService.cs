using Spotify.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Api.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Track>> Search(string input, string accessToken);
        Task<AudioFeatures> GetTrackFeatures(string id, string accesToken);
    }
}