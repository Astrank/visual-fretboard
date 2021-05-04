using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Spotify.Api.Models;

namespace Spotify.Api.Services
{
    public class SpotifyAccountService : ISpotifyAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SpotifyAccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken()
        {
            var tokenExpiryTime = _configuration["Spotify:AccessToken:ExpiryTime"];

            if (tokenExpiryTime != null &&
                DateTime.ParseExact(tokenExpiryTime, "dd/MM/yyyy H:mm:ss", null) > DateTime.Now)
            {
                return _configuration["Spotify:AccessToken:Token"];
            }
            else
            {
                await NewAccessToken();

                return _configuration["Spotify:AccessToken:Token"];
            }
        }

        public async Task NewAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    $"{_configuration["Spotify:ClientId"]}:{_configuration["Spotify:ClientSecret"]}"))
            );

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            });

            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AccessToken>(responseStream);

            var epiryTime = DateTime.Now.AddSeconds(authResult.expires_in);

            _configuration["Spotify:AccessToken:Token"] = authResult.access_token;
            _configuration["Spotify:AccessToken:ExpiryTime"] = epiryTime.ToString();
        }
    }
}