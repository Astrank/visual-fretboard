using Spotify.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotify.Api.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CustomTrack>> Search(string input, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"search?q={input.Replace(" ", "+")}&type=track&limit=50");
            
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<SearchResponse>(responseStream);

            return responseObject?.tracks?.items.Select(x => new CustomTrack
            {
                Id = x.id,
                Name = x.name,
                Album = x.album.name,
                Artist = x.artists.FirstOrDefault().name,
                ImageUrl = x.album.images.FirstOrDefault().url
            });
        }

        public async Task<AudioFeatures> GetTrackFeatures(string id, string accesToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesToken);

            var response = await _httpClient.GetAsync($"audio-features/{id}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AudioFeatures>(responseStream);
        }

        public async Task<IEnumerable<CustomTrack>> GetPlaylist(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"playlists/{id}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetPlaylistResponse>(responseStream);

            return responseObject?.tracks?.items.Select(x => new CustomTrack
            {
                Id = x.track.id,
                Name = x.track.name,
                Album = x.track.album.name,
                Artist = x.track.artists.FirstOrDefault().name,
                ImageUrl = x.track.album.images.FirstOrDefault().url
            });
        }
    }
}