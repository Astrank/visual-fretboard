using Spotify.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Api.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<CustomTrack>> Search(string input, string accessToken);
        Task<AudioFeatures> GetTrackFeatures(string id, string accessToken);
        Task<IEnumerable<CustomTrack>> GetPlaylist(string id, string accessToken);
    }
}