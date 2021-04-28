using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Spotify.Api.Models;
using Spotify.Api.Services;

namespace Spotify.Api.Controllers
{
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly ISpotifyService _spotifyService;
        private readonly IConfiguration _configuration;

        public SpotifyController(
            ISpotifyAccountService spotifyAccountService, 
            ISpotifyService spotifyService,
            IConfiguration configuration)
        {
            _spotifyAccountService = spotifyAccountService;
            _spotifyService = spotifyService;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CustomTrack>> Search(string input)
        {
            var token = await _spotifyAccountService.GetAccessToken();

            var search = await _spotifyService.Search(input, token);

            return search;
        }

        public async Task<AudioFeatures> Features(string id)
        {
            var token = await _spotifyAccountService.GetAccessToken();

            var features = await _spotifyService.GetTrackFeatures(id, token);

            return features;
        }

        public async Task<IEnumerable<CustomTrack>> Playlists(string id)
        {
            var token = await _spotifyAccountService.GetAccessToken();

            var playlist = await _spotifyService.GetPlaylist(id, token);

            return playlist;
        }
    }
}